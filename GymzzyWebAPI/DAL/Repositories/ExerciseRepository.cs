using GymzzyWebAPI.DAL.Repositories.Interfaces;
using GymzzyWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL.Repositories
{
    public class ExerciseRepository : GenericRepository<Exercise, WorkoutContext>, IExerciseRepository
    {
        private readonly WorkoutContext _context;
        public ExerciseRepository(WorkoutContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Exercise> GetByNameAsync(string exerciseName)
        {
            try
            {
                var exercise = await _context.Exercise.SingleOrDefaultAsync(p => p.Name == exerciseName);
                if (exercise is default(Exercise))
                {
                    return null;
                }

                return exercise;
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("More than one Exercises with the same name exists in context.");
            }
        }
    }
}
