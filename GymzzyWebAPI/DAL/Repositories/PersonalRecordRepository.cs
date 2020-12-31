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

        public async Task<PersonalRecord> GetUserOldRecord(Series series, Guid userId)
        {
            var record = await _context.PersonalRecord
                .Include(p => p.Series)
                .SingleOrDefaultAsync(p =>
                    (p.Series.Training.UserId == userId) &&
                    (p.Series.ExerciseId == series.ExerciseId) &&
                    (p.Series.Weight == series.Weight));

            if (record is default(PersonalRecord))
            {
                return _context.Add(new PersonalRecord()).Entity;
            }
            else
            {
                return (record.Series.Reps < series.Reps) ? record : default(PersonalRecord);
            }
        }

        public void DeleteAllUserRecords(Guid userId)
        {
            _context.PersonalRecord.RemoveRange(_context.PersonalRecord.Where(p=>p.Series.Training.UserId == userId));
        }

    }
}
