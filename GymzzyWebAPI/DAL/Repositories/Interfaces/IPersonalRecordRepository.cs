using GymzzyWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL.Repositories.Interfaces
{
    public interface IPersonalRecordRepository : IGenericRepository<PersonalRecord>
    {
        Task<PersonalRecord> GetUserRecordIfBeatenByCurrnetSetAsync(Set set, Guid userId);
        void DeleteAllUserRecords(Guid userId);
        Task<IEnumerable<Guid>> CheckIfRecordsBySetsIdsAsync(IEnumerable<Guid> setsIds);
    }
}
