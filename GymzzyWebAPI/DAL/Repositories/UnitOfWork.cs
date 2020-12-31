using GymzzyWebAPI.DAL.Repositories.Interfaces;
using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; }
        public ITrainingRepository Trainings { get; }
        public IExerciseRepository Exercise { get; }
        public IPersonalRecordRepository PersonalRecord { get; }
        public ISeriesRepository Series { get; }

        private readonly UserContext _userContext;
        private readonly WorkoutContext _workoutContext;

        public UnitOfWork(UserContext userContext, WorkoutContext workoutContext)
        {
            _userContext = userContext;
            _workoutContext = workoutContext;
            Users = new UserRepository(_userContext);
            Trainings = new TrainingRepository(_workoutContext);
            Exercise = new ExerciseRepository(_workoutContext);
            PersonalRecord = new PersonalRecordRepository(_workoutContext);
            Series = new SeriesRepository(_workoutContext);
        }

        public async Task<int> SaveChangesAsync()
        {
            int changesCount = await _userContext.SaveChangesAsync();
            changesCount += await _workoutContext.SaveChangesAsync();
            return changesCount;
        }
    }
}
