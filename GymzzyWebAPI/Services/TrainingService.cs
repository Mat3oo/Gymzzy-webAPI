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

            foreach (var item in training.Series)
            {
                try
                {
                    var exercise = await _unitOfWork.Exercise.GetByNameAsync(item.Exercise.Name);
                    if (exercise != null)
                    {
                        item.Exercise = exercise;
                    }
                }
                catch (InvalidOperationException)
                {
                    throw new SystemException("There are Exercise Names conflicts in context.");
                }
            }

            _unitOfWork.Trainings.Add(training);
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

            foreach (var item in training.Series)
            {
                try
                {
                    var exercise = await _unitOfWork.Exercise.GetByNameAsync(item.Exercise.Name);
                    if (exercise != null)
                    {
                        item.Exercise = exercise;
                    }
                }
                catch (InvalidOperationException)
                {
                    throw new SystemException("There are Exercise Names conflicts in context.");
                }
            }

            await _unitOfWork.SaveChangesAsync();

            return 0;
        }

        public async Task<IEnumerable<TrainingSimpleViewDTO>> GetUserTrainingsAsync(Guid userId)
        {
            var userTrainings = await _unitOfWork.Trainings.GetAll()
                .Where(p => p.UserId == userId)
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
    }
}
