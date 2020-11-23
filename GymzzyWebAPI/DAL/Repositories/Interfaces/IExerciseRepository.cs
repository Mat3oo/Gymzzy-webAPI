using GymzzyWebAPI.Models;

namespace GymzzyWebAPI.DAL.Repositories.Interfaces
{
    public interface IExerciseRepository : IGenericRepository<Exercise>
    {
        Exercise GetByName(string exerciseName);
    }
}
