using Models.DTO;
using System;
using System.Threading.Tasks;

namespace GymzzyWebAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDetailsViewDTO> GetUserDetailsAsync(Guid id);
    }
}
