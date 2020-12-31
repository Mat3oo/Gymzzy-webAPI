using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        ITrainingRepository Trainings { get; }
        IExerciseRepository Exercise { get; }
        IPersonalRecordRepository PersonalRecord { get; }
        ISeriesRepository Series { get; }

        Task<int> SaveChangesAsync();
    }
}
