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
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get user details.
        /// </summary>
        /// <returns>Currently authenticated user details.</returns>
        /// <response code="400">If the authenticated user id from token is invalid format.</response>
        /// <response code="404">If the authenticated user doesn't exist.</response>
        /// <response code="200">Authenticated user details.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ErrorResposneBodyDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResposneBodyDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserDetailsViewDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUser()
        {
            var parseResult = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

            if (!parseResult)
            {
                return BadRequest(new ErrorResposneBodyDTO
                {
                    DeveloperMessage = $"Invalid user id format. Given id: \"{userId}\", try to get new token.",
                    UserMessage = $"Try to relogin."
                });
            }

            var userDetails = await _userService.GetUserDetailsAsync(userId);

            if (userDetails == null)
            {
                return NotFound(new ErrorResposneBodyDTO
                {
                    DeveloperMessage = $"User with id: \"{userId}\" doesn't exist."
                });
            }

            return Ok(userDetails);
        }

        /// <summary>
        /// Update user details.
        /// </summary>
        /// <param name="userDetails"></param>
        /// <returns></returns>
        /// <response code="400">If the authenticated user id from token is invalid format.</response>
        /// <response code="422">If the model is fine formed, but problems with values.</response>
        /// <response code="204">User details updated.</response>
        /// <response code="404">If the authenticated user doesn't exist.</response>
        [HttpPut]
        [ProducesResponseType(typeof(ErrorResposneBodyDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Identity.IdentityError), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResposneBodyDTO), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(UserDetailsEditDTO userDetails)
        {
            var parseResult = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

            if (!parseResult)
            {
                return BadRequest(new ErrorResposneBodyDTO
                {
                    DeveloperMessage = $"Invalid user id format. Given id: \"{userId}\", try to get new token.",
                    UserMessage = $"Try to relogin."
                });
            }

            short result;
            try
            {
                result = await _userService.UpdateUserDetailsAsync(userId, userDetails);
            }
            catch (ArgumentException e)
            {
                return UnprocessableEntity(e.Data["Errors"]);
            }

            return result switch
            {
                0 => NoContent(),
                1 => NotFound(new ErrorResposneBodyDTO
                {
                    DeveloperMessage = $"User with id: \"{userId}\" doesn't exist."
                }),
                _ => throw new NotImplementedException()
            };
        }

        /// <summary>
        /// Register user in the system.
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>Details of registered user.</returns>
        /// <response code="422">If the model is fine formed, but problems with values.</response>
        /// <response code="201">Created user details.</response>
        [HttpPost]
        [AllowAnonymous]
        [Route("regist")]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Identity.IdentityError), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(UserDetailsViewDTO), StatusCodes.Status201Created)]
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

        /// <summary>
        /// Generate user bearer token.
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>Generated JWT user token.</returns>
        /// <response code="401">If user email or password is invalid.</response>
        /// <response code="200">Generated user bearer token.</response>
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        [ProducesResponseType(typeof(ErrorResposneBodyDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(TokenDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginJwtToken(UserLoginDTO userDTO)
        {
            var tokenString = await _userService.LoginUserAsync(userDTO);
            if (tokenString is null)
            {
                return Unauthorized(new ErrorResposneBodyDTO
                {
                    UserMessage = "Invalid email or password."
                });
            }

            return Ok(new TokenDTO
            {
                AccessToken = tokenString
            });
        }
    }
}
