using GymzzyWebAPI.Models;
using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL.Repositories.Interfaces
{
    public interface IExerciseRepository : IGenericRepository<Exercise>
    {
        Task<Exercise> GetByNameAsync(string exerciseName);
    }
}
