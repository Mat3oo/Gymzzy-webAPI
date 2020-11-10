﻿using GymzzyWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
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
    }
}