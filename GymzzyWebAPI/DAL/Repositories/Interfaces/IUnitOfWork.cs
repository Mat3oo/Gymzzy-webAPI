using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        ITrainingRepository Trainings { get; }
        IExerciseRepository Exercise { get; }

        Task<int> SaveChangesAsync();
    }
}
