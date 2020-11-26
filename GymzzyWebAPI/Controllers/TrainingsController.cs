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

        [HttpGet("{trainingId}")]
        public async Task<IActionResult> GetTraining(Guid trainingId)
        {
            var parseResult = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid id);

            if (!parseResult)
            {
                return BadRequest($"Invalid user id format. Given id: \"{id}\", try to relogin");
            }

            var trainingById = await _trainingService.GetUserTrainingByIdAsync(id, trainingId);
            if (trainingById is null)
            {
                return NotFound($"User doesn't have training with id: \"{trainingId}\".");
            }

            return Ok(trainingById);
        }

        [HttpGet]
        public async Task<IActionResult> GetTrainings()
        {
            var parseResult = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid id);

            if (!parseResult)
            {
                return BadRequest($"Invalid user id format. Given id: \"{id}\", try to relogin");
            }

            return Ok(await _trainingService.GetUserTrainingsAsync(id));
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

            return CreatedAtAction(nameof(GetTraining), new { trainingId = createdTraining.Id }, createdTraining);
        }

        [HttpPut("{trainingId}")]
        public async Task<IActionResult> UpdateTraining(Guid trainingId, TrainingEditDTO training)
        {
            var parseResult = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid id);

            if (!parseResult)
            {
                return BadRequest($"Invalid user id format. Given id: \"{id}\", try to relogin");
            }

            var result = await _trainingService.UpdateTrainingAsync(id, trainingId, training);

            return result switch
            {
                0 => NoContent(),
                1 => NotFound($"User with id: \"{id}\" doesn't exist."),
                2 => NotFound($"User doesn't have training with id: \"{trainingId}\"."),
                3 => Conflict($"Inconsistent Series Id/Ids."),
                _ => throw new NotImplementedException()
            };
        }
    }
}
