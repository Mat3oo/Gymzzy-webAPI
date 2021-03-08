using GymzzyWebAPI.DAL.Repositories.Interfaces;
using GymzzyWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL.Repositories
{
    public class PersonalRecordRepository : GenericRepository<PersonalRecord, WorkoutContext>, IPersonalRecordRepository
    {
        private readonly WorkoutContext _context;

        public PersonalRecordRepository(WorkoutContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PersonalRecord> GetUserRecordIfBeatenByCurrnetSetAsync(Set set, Guid userId)
        {
            var record = await _context.PersonalRecord
                .SingleOrDefaultAsync(p =>
                    (p.Set.Exercise.Training.UserId == userId) &&
                    (p.Set.Exercise.ExerciseDetailsId == set.Exercise.ExerciseDetailsId) &&
                    (p.Set.Weight == set.Weight));

            if (record is default(PersonalRecord))
            {
                return _context.Add(new PersonalRecord()).Entity;
            }
            else
            {
                return (record.Set.Reps < set.Reps) ? record : default;
            }
        }

        public void DeleteAllUserRecords(Guid userId)
        {
            _context.PersonalRecord.RemoveRange(_context.PersonalRecord.Where(p => p.Set.Exercise.Training.UserId == userId));
        }

        public async Task<IEnumerable<Guid>> CheckIfRecordsBySetsIdsAsync(IEnumerable<Guid> setsIds)
        {
            return await _context.PersonalRecord
                .Where(p => setsIds.Contains(p.SetId))
                .Select(p => p.SetId)
                .ToArrayAsync();
        }
    }
}
