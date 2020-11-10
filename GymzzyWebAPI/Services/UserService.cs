using GymzzyWebAPI.DAL.Repositories.Interfaces;
using GymzzyWebAPI.Models;
using GymzzyWebAPI.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace GymzzyWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetUserDetailsAsync(Guid id)
        {
            return await _unitOfWork.Users.GetAsync(id);
        }
    }
}
