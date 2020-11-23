using GymzzyWebAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymzzyWebAPI.Services.Interfaces
{
    public interface ITrainingService
    {
        Task<TrainingViewDTO> AddTrainingAsync(Guid userId, TrainingCreateDTO trainingDTO);
        Task<IEnumerable<TrainingSimpleViewDTO>> GetUserTrainingsAsync(Guid userId);
    }
}
