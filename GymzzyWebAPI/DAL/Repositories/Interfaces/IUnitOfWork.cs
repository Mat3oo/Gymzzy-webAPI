using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public IUserRepository Users { get; }
        Task<int> SaveChangesAsync();
    }
}
