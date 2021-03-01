using GymzzyWebAPI.DAL.Repositories.Interfaces;
using GymzzyWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL.Repositories
{
    public class ExerciseDetailsRepository : GenericRepository<ExerciseDetails, WorkoutContext>, IExerciseDetailsRepository
    {
        private readonly WorkoutContext _context;
        public ExerciseDetailsRepository(WorkoutContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ExerciseDetails> GetByExerciseNameAsync(string exerciseName)
        {
            try
            {
                var exercise = await _context.ExerciseDetails.SingleOrDefaultAsync(p => p.Name == exerciseName);
                if (exercise is default(ExerciseDetails))
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

        public async Task<IEnumerable<ExerciseDetails>> GetAllByExerciseNamesAsync(IEnumerable<string> exercisesNames)
        {
            return await _context.ExerciseDetails
                .Where(p => exercisesNames.Contains(p.Name))
                .ToArrayAsync();
        }
    }
}
