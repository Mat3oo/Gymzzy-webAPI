using GymzzyWebAPI.DAL.Repositories.Interfaces;
using GymzzyWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL.Repositories
{
    public class SeriesRepository : GenericRepository<Series, WorkoutContext>, ISeriesRepository
    {
        private readonly WorkoutContext _context;

        public SeriesRepository(WorkoutContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Guid>> GetNewPersonalRecordsSeriesIdsAsync(Guid userId)
        {
            var allSeriesWithMaxReps = await _context.Series
               .Where(p => p.Training.UserId == userId)
               .OrderBy(p => p.Training.Date)
               .GroupBy(p => new { p.ExerciseId, p.Weight })
               .Select(p => new { p.Key, Reps = p.Max(m => m.Reps) })
               .Join(_context.Series,
                    grouped => new { grouped.Key.ExerciseId, grouped.Key.Weight, grouped.Reps },
                    dbSeries => new { dbSeries.ExerciseId, dbSeries.Weight, dbSeries.Reps },
                    (grouped, dbseries) => new Series { Id = dbseries.Id, Weight = dbseries.Weight, Reps = dbseries.Reps })
               .ToArrayAsync();

            return allSeriesWithMaxReps.GroupBy(p => new { p.ExerciseId, p.Weight })
                .SelectMany(p => p.Where(w => w.Reps == p.Max(m => m.Reps)).Take(1))
                .Select(p => p.Id)
                .ToArray();
        }
    }
}
