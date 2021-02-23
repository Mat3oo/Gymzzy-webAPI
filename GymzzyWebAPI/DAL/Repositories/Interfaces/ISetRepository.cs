using GymzzyWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL.Repositories.Interfaces
{
    public interface ISetRepository : IGenericRepository<Set>
    {
        public Task<IEnumerable<Guid>> FindNewPersonalRecordsSetsIdsAsync(Guid userId);
    }
}
