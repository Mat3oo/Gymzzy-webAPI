using GymzzyWebAPI.Models.DTO;
using System;
using System.Threading.Tasks;

namespace GymzzyWebAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDetailsViewDTO> GetUserDetailsAsync(Guid id);
        Task<short> UpdateUserDetailsAsync(Guid id, UserDetailsEditDTO userDetails);
        Task<UserDetailsViewDTO> RegisterUserAsync(UserRegistDTO userRegist);
        Task<string> LoginUserAsync(UserLoginDTO userLogin);
    }
}
