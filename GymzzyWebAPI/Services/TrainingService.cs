using AutoMapper;
using GymzzyWebAPI.DAL.Repositories.Interfaces;
using GymzzyWebAPI.Models;
using GymzzyWebAPI.Models.DTO;
using GymzzyWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymzzyWebAPI.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TrainingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<TrainingViewDTO> AddTrainingAsync(Guid userId, TrainingCreateDTO trainingDTO)
        {
            var user = await _unitOfWork.Users.GetAsync(userId);
            if (user is null)
            {
                return null;
            }

            var training = _mapper.Map<Training>(trainingDTO);
            training.UserId = userId;

            await AssignExistingExerciseDetails(training.Exercises);

            _unitOfWork.Trainings.Add(training);

            await CalculatePersonalRecords(training.Exercises, userId);

            await _unitOfWork.SaveChangesAsync();

            var mapped = _mapper.Map<TrainingViewDTO>(training);

            return await MarkAsRecordsAsync(mapped);
        }

        public async Task<short> UpdateTrainingAsync(Guid userId, Guid trainingId, TrainingEditDTO trainingDTO)
        {
            var user = await _unitOfWork.Users.GetAsync(userId);
            if (user is null)
            {
                return 1;
            }

            var training = await _unitOfWork.Trainings.GetWithDetailsAsync(trainingId);
            if (training is null || training.UserId != userId)
            {
                return 2;
            }

            #region Verify Exercise Ids
            var allExerciseIds = training.Exercises
                .Select(p => p.Id as Guid?)
                .Concat(new Guid?[] { null });

            if (!trainingDTO.Exercises.All(p => allExerciseIds.Contains(p.Id)))
            {
                return 3;
            }
            #endregion

            #region Verify Set Ids
            var allSetIds = training.Exercises
                .SelectMany(p => p.Sets.Select(s => s.Id as Guid?))
                .Concat(new Guid?[] { null });

            var allDTOSetIds = trainingDTO.Exercises
                .SelectMany(p => p.Sets.Select(s => s.Id));

            if (!allDTOSetIds.All(p => allSetIds.Contains(p)))
            {
                return 3;
            }
            #endregion Verify Set Ids

            _mapper.Map(trainingDTO, training);

            await AssignExistingExerciseDetails(training.Exercises);

            await RecalculatePersonalRecordsAsync(userId);

            await _unitOfWork.SaveChangesAsync();

            return 0;
        }

        public async Task<IEnumerable<TrainingSimpleViewDTO>> GetUserTrainingsAsync(Guid userId)
        {
            var userTrainings = await _unitOfWork.Trainings.GetAll()
                .Where(p => p.UserId == userId)
                .OrderByDescending(k => k.Date)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TrainingSimpleViewDTO>>(userTrainings);
        }

        public async Task<TrainingViewDTO> GetUserTrainingByIdAsync(Guid userId, Guid trainingId)
        {
            var training = await _unitOfWork.Trainings.GetWithDetailsAsync(trainingId);
            if (training is default(Training) || training.UserId != userId)
            {
                return null;
            }

            var mapped = _mapper.Map<TrainingViewDTO>(training);

            return await MarkAsRecordsAsync(mapped);
        }

        public async Task DeleteUserTrainingAsync(Guid userId, Guid trainingId)
        {
            var training = await _unitOfWork.Trainings.GetAsync(trainingId);
            if (training != null && training.UserId == userId)
            {
                _unitOfWork.Trainings.Delete(training);

                await RecalculatePersonalRecordsAsync(userId);

                await _unitOfWork.SaveChangesAsync();
            }
        }

        private async Task AssignExistingExerciseDetails(IEnumerable<Exercise> exercises)
        {
            var exercisesNames = exercises.Select(p => p.ExerciseDetails.Name);

            var savedExerciseDetails = await _unitOfWork.ExerciseDetails.GetAllByExerciseNamesAsync(exercisesNames);

            var newExercises = new Dictionary<string, ExerciseDetails>();
            foreach (var item in exercises)
            {
                try
                {
                    var exercise = savedExerciseDetails.SingleOrDefault(p => p.Name == item.ExerciseDetails.Name);
                    if (exercise != default)
                    {
                        item.ExerciseDetails = exercise;
                        item.ExerciseDetailsId = exercise.Id;
                    }
                    else
                    {
                        newExercises.TryAdd(item.ExerciseDetails.Name, item.ExerciseDetails);
                        item.ExerciseDetails = newExercises[item.ExerciseDetails.Name];
                        item.ExerciseDetailsId = newExercises[item.ExerciseDetails.Name].Id;
                    }
                }
                catch (InvalidOperationException)
                {
                    throw new SystemException("There are Exercise Names conflicts in context.");
                }
            }
        }
        private async Task CalculatePersonalRecords(IEnumerable<Exercise> exercises, Guid userId)
        {
            List<Set> filteredMaxes = new List<Set>();

            foreach (var exercise in exercises)
            {
                filteredMaxes.AddRange(exercise.Sets.GroupBy(p => p.Weight)
                    .SelectMany(p => p.Where(w => w.Reps == p.Max(m => m.Reps)).Take(1)));
            }

            foreach (var set in filteredMaxes)
            {
                var oldRecordToUpdate = await _unitOfWork.PersonalRecord.GetUserRecordIfBeatenByCurrnetSetAsync(set, userId);
                if (oldRecordToUpdate != default(PersonalRecord))
                {
                    oldRecordToUpdate.Set = set;
                }
            }
        }

        private async Task RecalculatePersonalRecordsAsync(Guid userId)
        {
            await _unitOfWork.SaveChangesAsync();

            var newPersonalRecordsSets = await _unitOfWork.Set.FindNewPersonalRecordsIdsAsync(userId);

            var newPersonalRecords = new List<PersonalRecord>(newPersonalRecordsSets.Count());

            foreach (var item in newPersonalRecordsSets)
            {
                newPersonalRecords.Add(new PersonalRecord { SetId = item });
            }

            _unitOfWork.PersonalRecord.DeleteAllUserRecords(userId);

            _unitOfWork.PersonalRecord.AddRange(newPersonalRecords.ToArray());
        }

        private async Task<TrainingViewDTO> MarkAsRecordsAsync(TrainingViewDTO trainingViewDTO)
        {
            var setsIds = trainingViewDTO.Exercises.SelectMany(p => p.Sets.Select(s => s.Id));

            var checkedRecordsIds = await _unitOfWork.PersonalRecord.CheckIfRecordsBySetsIdsAsync(setsIds);

            var DTOSetsToMark = trainingViewDTO.Exercises.SelectMany(p => p.Sets.Where(s => checkedRecordsIds.Contains(s.Id)));

            foreach (var set in DTOSetsToMark)
            {
                set.Record = true;
            }

            return trainingViewDTO;
        }
    }
}
