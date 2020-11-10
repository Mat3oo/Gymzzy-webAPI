using GymzzyWebAPI.Models;
using System;
using System.Threading.Tasks;

namespace GymzzyWebAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserDetailsAsync(Guid id);
    }
}
