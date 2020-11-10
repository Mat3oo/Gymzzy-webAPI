using GymzzyWebAPI.DAL.Repositories.Interfaces;
using System.Threading.Tasks;

namespace GymzzyWebAPI.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; }

        private readonly UserContext _userContext;

        public UnitOfWork(UserContext userContext)
        {
            _userContext = userContext;
            Users = new UserRepository(_userContext);
        }

        public void Dispose()
        {
            _userContext.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await _userContext.SaveChangesAsync();
        }
    }
}
