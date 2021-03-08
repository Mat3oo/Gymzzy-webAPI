using GymzzyWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL.Repositories.Interfaces
{
    public interface IExerciseDetailsRepository : IGenericRepository<ExerciseDetails>
    {
        Task<ExerciseDetails> GetByExerciseNameAsync(string exerciseName);
        Task<IEnumerable<ExerciseDetails>> GetAllByExerciseNamesAsync(IEnumerable<string> exercisesNames);

    }
}
