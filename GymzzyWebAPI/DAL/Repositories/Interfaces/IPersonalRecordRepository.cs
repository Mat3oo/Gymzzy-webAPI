using GymzzyWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL.Repositories.Interfaces
{
    public interface IPersonalRecordRepository : IGenericRepository<PersonalRecord>
    {
        Task<PersonalRecord> GetUserOldRecord(Series series, Guid userId);
        void DeleteAllUserRecords(Guid userId);
        Task<IEnumerable<Guid>> CheckRecordsBySeriesIdsAsync(IEnumerable<Guid> seriesIds);
    }
}
