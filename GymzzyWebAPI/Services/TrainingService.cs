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

            await AssignExistingExercises(training.Series);

            _unitOfWork.Trainings.Add(training);

            await CalculatePersonalRecords(training.Series, userId);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TrainingViewDTO>(training);
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

            foreach (var item in trainingDTO.Series)
            {
                if (!training.Series.Any(p => p.Id == item.Id) && item.Id != null)
                {
                    return 3;
                };
            }

            _mapper.Map(trainingDTO, training);

            await AssignExistingExercises(training.Series);

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

            return _mapper.Map<TrainingViewDTO>(training);
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

        private async Task AssignExistingExercises(IEnumerable<Series> series)
        {
            var newExercises = new Dictionary<string, Exercise>();

            foreach (var item in series)
            {
                try
                {
                    var exercise = await _unitOfWork.Exercise.GetByNameAsync(item.Exercise.Name);
                    if (exercise != null)
                    {
                        item.Exercise = exercise;
                        item.ExerciseId = exercise.Id;
                    }
                    else
                    {
                        newExercises.TryAdd(item.Exercise.Name, item.Exercise);
                        item.Exercise = newExercises[item.Exercise.Name];
                        item.ExerciseId = newExercises[item.Exercise.Name].Id;
                    }
                }
                catch (InvalidOperationException)
                {
                    throw new SystemException("There are Exercise Names conflicts in context.");
                }
            }
        }
        private async Task CalculatePersonalRecords(IEnumerable<Series> series, Guid userId)
        {
            var filteredMaxes = series.GroupBy(p => new { p.ExerciseId, p.Weight })
                .SelectMany(p => p.Where(m => m.Reps == p.Max(r => r.Reps)));

            foreach (var item in filteredMaxes)
            {
                var oldRecord = await _unitOfWork.PersonalRecord.GetUserOldRecord(item, userId);
                if (oldRecord != default(PersonalRecord))
                {
                    oldRecord.Series = item;
                }
            }
        }

        private async Task RecalculatePersonalRecordsAsync(Guid userId)
        {
            await _unitOfWork.SaveChangesAsync();

            var newPersonalRecordsSeries = await _unitOfWork.Series.GetNewPersonalRecordsSeriesIdsAsync(userId);

            var newPersonalRecords = new List<PersonalRecord>(newPersonalRecordsSeries.Count());

            foreach (var item in newPersonalRecordsSeries)
            {
                newPersonalRecords.Add(new PersonalRecord { SeriesId = item });
            }

            _unitOfWork.PersonalRecord.DeleteAllUserRecords(userId);

            _unitOfWork.PersonalRecord.AddRange(newPersonalRecords.ToArray());
        }
    }
}
