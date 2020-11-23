using AutoMapper;
using GymzzyWebAPI.DAL.Repositories.Interfaces;
using GymzzyWebAPI.Models;
using GymzzyWebAPI.Models.DTO;
using GymzzyWebAPI.Services.Interfaces;
using System;
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
    }
}
