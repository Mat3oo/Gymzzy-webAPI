using GymzzyWebAPI.Models.DTO;
using GymzzyWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GymzzyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<UserDetailsViewDTO> GetUser([FromBody] Guid id)
        {
            return await _userService.GetUserDetailsAsync(id);
        }

        [HttpPost]
        [Route("regist")]
        public async Task<IActionResult> Regist(UserRegistDTO userDTO)
        {
            UserDetailsViewDTO created;

            try
            {
                created = await _userService.RegisterUserAsync(userDTO);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Data["Errors"]);
            }

            return Created($"api/user", created);
        }
    }
}
