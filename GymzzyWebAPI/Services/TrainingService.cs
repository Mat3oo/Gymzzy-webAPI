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
                var exercise = _unitOfWork.Exercise.GetByName(item.Exercise.Name);
                if (exercise != null)
                {
                    item.Exercise = exercise;
                }
            }

            await _unitOfWork.Trainings.AddAsync(training);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TrainingViewDTO>(training);
        }

        public async Task<IEnumerable<TrainingSimpleViewDTO>> GetUserTrainingsAsync(Guid userId)
        {
            var userTrainings = await _unitOfWork.Trainings.GetAll()
                .Where(p => p.UserId == userId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TrainingSimpleViewDTO>>(userTrainings);
        }
    }
}
