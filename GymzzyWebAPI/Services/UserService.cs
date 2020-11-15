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
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public UserService(IUnitOfWork unitOfWork,
                           IMapper mapper,
                           UserManager<User> userManager,
                           SignInManager<User> signInManager,
                           ITokenGeneratorService tokenGeneratorService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGeneratorService = tokenGeneratorService;
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

        public async Task<string> LoginUserAsync(UserLoginDTO userLogin)
        {
            var user = await _userManager.FindByEmailAsync(userLogin.Email);

            if (user is null)
            {
                return null;
            }

            var passCheck = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);

            if(!passCheck.Succeeded)
            {
                return null;
            }

            return _tokenGeneratorService.GenerateToken(user);
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
