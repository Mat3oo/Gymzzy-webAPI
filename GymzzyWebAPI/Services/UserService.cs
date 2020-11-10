using AutoMapper;
using GymzzyWebAPI.DAL.Repositories.Interfaces;
using GymzzyWebAPI.Models;
using GymzzyWebAPI.Models.DTO;
using GymzzyWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace GymzzyWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<UserDetailsViewDTO> GetUserDetailsAsync(Guid id)
        {
            var temp = await _unitOfWork.Users.GetAsync(id);

            UserDetailsViewDTO userDTO = null;

            if (temp != null)
            {
                userDTO = _mapper.Map<UserDetailsViewDTO>(temp);
            }

            return userDTO;
        }

        public async Task<UserDetailsViewDTO> RegisterUserAsync(UserRegistDTO userRegist)
        {
            var user = _mapper.Map<User>(userRegist);
            var result = await _userManager.CreateAsync(user, userRegist.Password);

            if (!result.Succeeded)
            {
                var ex = new ArgumentException();
                ex.Data["Errors"] = result.Errors;
                throw ex;
            }

            return _mapper.Map<UserDetailsViewDTO>(user);
        }
    }
}
