using GymzzyWebAPI.Models.DTO;
using GymzzyWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
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
        public async Task<IActionResult> GetUser()
        {
            var parseResult = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid id);

            if (!parseResult)
            {
                return BadRequest($"Invalid user id format. Given id: \"{id}\", try to relogin");
            }

            var userDetails = await _userService.GetUserDetailsAsync(id);

            if (userDetails == null)
            {
                return NotFound($"User with id: \"{id}\" doesn't exist.");
            }

            return Ok(userDetails);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDetailsEditDTO userDetails)
        {
            var parseResult = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid id);

            if (!parseResult)
            {
                return BadRequest($"Invalid user id format. Given id: \"{id}\", try to relogin");
            }

            short result;
            try
            {
                result = await _userService.UpdateUserDetailsAsync(id, userDetails);
            }
            catch (ArgumentException e)
            {
                return UnprocessableEntity(e.Data["Errors"]);
            }

            return result switch
            {
                0 => NoContent(),
                1 => NotFound($"User with id: \"{id}\" doesn't exist."),
                _ => StatusCode(StatusCodes.Status500InternalServerError, "Sommething goes wrong.")
            };
        }

        [HttpPost]
        [AllowAnonymous]
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
                return UnprocessableEntity(e.Data["Errors"]);
            }

            return CreatedAtAction(nameof(GetUser), created);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> LoginJwtToken(UserLoginDTO userDTO)
        {
            var tokenString = await _userService.LoginUserAsync(userDTO);
            if (tokenString is null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            return Ok(new { token = tokenString });
        }
    }
}
