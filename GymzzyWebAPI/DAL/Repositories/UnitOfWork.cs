using GymzzyWebAPI.DAL.Repositories.Interfaces;
using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; }
        public ITrainingRepository Trainings { get; }
        public IExerciseDetailsRepository ExerciseDetails { get; }
        public IPersonalRecordRepository PersonalRecord { get; }
        public ISetRepository Set { get; }

        private readonly UserContext _userContext;
        private readonly WorkoutContext _workoutContext;

        public UnitOfWork(UserContext userContext, WorkoutContext workoutContext)
        {
            _userContext = userContext;
            _workoutContext = workoutContext;
            Users = new UserRepository(_userContext);
            Trainings = new TrainingRepository(_workoutContext);
            ExerciseDetails = new ExerciseDetailsRepository(_workoutContext);
            PersonalRecord = new PersonalRecordRepository(_workoutContext);
            Set = new SetRepository(_workoutContext);
        }

        public async Task<int> SaveChangesAsync()
        {
            int changesCount = await _userContext.SaveChangesAsync();
            changesCount += await _workoutContext.SaveChangesAsync();
            return changesCount;
        }
    }
}
