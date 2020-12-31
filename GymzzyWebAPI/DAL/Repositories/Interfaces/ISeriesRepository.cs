using GymzzyWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL.Repositories.Interfaces
{
    public interface ISeriesRepository : IGenericRepository<Series>
    {
        public Task<IEnumerable<Guid>> GetNewPersonalRecordsSeriesIdsAsync(Guid userId);
    }
}
