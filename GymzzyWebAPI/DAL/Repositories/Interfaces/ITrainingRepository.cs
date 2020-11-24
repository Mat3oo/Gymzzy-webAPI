using GymzzyWebAPI.Models;
using System;
using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL.Repositories.Interfaces
{
    public interface ITrainingRepository : IGenericRepository<Training>
    {
        Task<Training> GetWithDetailsAsync(Guid id);
    }
}
