using GymzzyWebAPI.DAL.Repositories.Interfaces;
using GymzzyWebAPI.Models;

namespace GymzzyWebAPI.DAL.Repositories
{
    public class UserRepository : GenericRepository<User, UserContext>, IUserRepository
    {
        public UserRepository(UserContext context) : base(context)
        {
        }
    }
}
