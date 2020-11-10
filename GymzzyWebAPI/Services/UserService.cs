using GymzzyWebAPI.DAL.Repositories.Interfaces;
using GymzzyWebAPI.Services.Interfaces;
using Models.DTO;
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

        public async Task<UserDetailsViewDTO> GetUserDetailsAsync(Guid id)
        {
            var temp = await _unitOfWork.Users.GetAsync(id);

            UserDetailsViewDTO userDTO = null;

            if (temp != null)
            {
                userDTO = new UserDetailsViewDTO
                {
                    Id = temp.Id,
                    Name = temp.Name,
                    LastName = temp.LastName,
                    Nick = temp.Nick,
                    Email = temp.Email,
                    Gender = temp.Gender,
                    Height = temp.Height,
                    Weight = temp.Weight,
                    Birthday = temp.Birthday
                };
            }

            return userDTO;
        }
    }
}
