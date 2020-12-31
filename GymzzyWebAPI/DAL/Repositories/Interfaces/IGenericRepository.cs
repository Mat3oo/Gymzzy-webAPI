using System;
using System.Linq;
using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(Guid id);
        IQueryable<TEntity> GetAll();
        void Add(TEntity entity);
        void AddRange(TEntity[] entities);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
