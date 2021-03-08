using GymzzyWebAPI.DAL.Repositories.Interfaces;
using GymzzyWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL.Repositories
{
    public class TrainingRepository : GenericRepository<Training, WorkoutContext>, ITrainingRepository
    {
        private readonly WorkoutContext _context;

        public TrainingRepository(WorkoutContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Training> GetWithDetailsAsync(Guid id)
        {
            return await _context.Training
                .Include(p => p.Exercises).ThenInclude(p => p.Sets)
                .Include(p => p.Exercises).ThenInclude(p => p.ExerciseDetails)
                .SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}
