using GymzzyWebAPI.Models.DTO;
using GymzzyWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GymzzyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingsController : ControllerBase
    {
        private readonly ITrainingService _trainingService;

        public TrainingsController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTrainingAsync(TrainingCreateDTO training)
        {
            var parseResult = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid id);

            if (!parseResult)
            {
                return BadRequest($"Invalid user id format. Given id: \"{id}\", try to relogin");
            }

            var createdTraining = await _trainingService.AddTrainingAsync(id, training);
            if (createdTraining is null)
            {
                return NotFound($"User with id: \"{id}\" doesn't exist.");
            }

            return Ok(createdTraining);
        }
    }
}
