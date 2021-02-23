using GymzzyWebAPI.Models;
using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL.Repositories.Interfaces
{
    public interface IExerciseDetailsRepository : IGenericRepository<ExerciseDetails>
    {
        Task<ExerciseDetails> GetByNameAsync(string exerciseName);
    }
}
