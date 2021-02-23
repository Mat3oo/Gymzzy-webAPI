using GymzzyWebAPI.DAL.Repositories.Interfaces;
using GymzzyWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL.Repositories
{
    public class SetRepository : GenericRepository<Set, WorkoutContext>, ISetRepository
    {
        private readonly WorkoutContext _context;

        public SetRepository(WorkoutContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Guid>> FindNewPersonalRecordsSetsIdsAsync(Guid userId)
        {
            var allSetsWithMaxReps = await _context.Set
               .Where(p => p.Exercise.Training.UserId == userId)
               .OrderBy(p => p.Exercise.Training.Date)
               .GroupBy(p => new { p.ExerciseId, p.Weight })
               .Select(p => new { p.Key, Reps = p.Max(m => m.Reps) })
               .Join(_context.Set,
                    grouped => new { grouped.Key.ExerciseId, grouped.Key.Weight, grouped.Reps },
                    dbSet => new { dbSet.ExerciseId, dbSet.Weight, dbSet.Reps },
                    (grouped, dbSets) => new Set { Id = dbSets.Id, Weight = dbSets.Weight, Reps = dbSets.Reps })
               .ToArrayAsync();

            return allSetsWithMaxReps.GroupBy(p => new { p.ExerciseId, p.Weight })
                .SelectMany(p => p.Where(w => w.Reps == p.Max(m => m.Reps)).Take(1))
                .Select(p => p.Id)
                .ToArray();
        }
    }
}
