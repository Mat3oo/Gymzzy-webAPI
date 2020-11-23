using GymzzyWebAPI.DAL.Repositories.Interfaces;
using GymzzyWebAPI.Models;

namespace GymzzyWebAPI.DAL.Repositories
{
    public class TrainingRepository : GenericRepository<Training, WorkoutContext>, ITrainingRepository
    {
        public TrainingRepository(WorkoutContext context) : base(context)
        {
        }
    }
}
