using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        ITrainingRepository Trainings { get; }
        IExerciseDetailsRepository ExerciseDetails { get; }
        IPersonalRecordRepository PersonalRecord { get; }
        ISetRepository Set { get; }

        Task<int> SaveChangesAsync();
    }
}
