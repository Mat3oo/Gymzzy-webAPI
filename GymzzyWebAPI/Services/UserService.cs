using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public UserService(IMapper mapper,
                           UserManager<User> userManager,
                           SignInManager<User> signInManager,
                           ITokenGeneratorService tokenGeneratorService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGeneratorService = tokenGeneratorService;
        }

        public async Task<UserDetailsViewDTO> GetUserDetailsAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user is null)
            {
                return null;
            }

            return _mapper.Map<UserDetailsViewDTO>(user);
        }

        public async Task<short> UpdateUserDetailsAsync(Guid id, UserDetailsEditDTO userDetails)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user is null)
            {
                return 1;
            }

            _mapper.Map(userDetails, user);

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                var ex = new ArgumentException();
                ex.Data["Errors"] = updateResult.Errors;
                throw ex;
            }

            return 0;
        }

        public async Task<string> LoginUserAsync(UserLoginDTO userLogin)
        {
            var user = await _userManager.FindByEmailAsync(userLogin.Email);

            if (user is null)
            {
                return null;
            }

            var passCheck = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);

            if (!passCheck.Succeeded)
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
