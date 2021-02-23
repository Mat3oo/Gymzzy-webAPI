using GymzzyWebAPI.Models.DTO;
using GymzzyWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GymzzyWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingsController : ControllerBase
    {
        private readonly ITrainingService _trainingService;

        public TrainingsController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        /// <summary>
        /// Get training details.
        /// </summary>
        /// <param name="trainingId">Guid id</param>
        /// <returns>Desirable by id training details.</returns>
        /// <response code="400">If the authenticated user id from token is invalid format.</response>
        /// <response code="404">If the authenticated user doesn't have desirable training.</response>
        /// <response code="200">Authenticated user training details.</response>
        [HttpGet("{trainingId}")]
        [ProducesResponseType(typeof(ErrorResposneBodyDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResposneBodyDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TrainingViewDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTraining(Guid trainingId)
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

            var trainingById = await _trainingService.GetUserTrainingByIdAsync(userId, trainingId);
            if (trainingById is null)
            {
                return NotFound(new ErrorResposneBodyDTO
                {
                    DeveloperMessage = $"User doesn't have training with id: \"{trainingId}\"."
                });
            }

            return Ok(trainingById);
        }

        /// <summary>
        /// Get all trainings.
        /// </summary>
        /// <returns>All user trainings.</returns>
        /// <response code="400">If the authenticated user id from token is invalid format.</response>
        /// <response code="200">All authenticated user trainings.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ErrorResposneBodyDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<TrainingSimpleViewDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTrainings()
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

            return Ok(await _trainingService.GetUserTrainingsAsync(userId));
        }

        /// <summary>
        /// Add training.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/trainings
        ///     {
        ///         "date": "2020-11-11T00:00:00",
        ///         "exercises": [
        ///             {
        ///                 "name": "name",
        ///                 "sets": [
        ///                     {
        ///                         "weight": 111.1,
        ///                         "reps": 1
        ///                     }
        ///                 ]
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <param name="training"></param>
        /// <returns>Created training details.</returns>
        /// <response code="400">If the authenticated user id from token is invalid format.</response>
        /// <response code="404">If the authenticated user no longer exists.</response>
        /// <response code="201">Created training details.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ErrorResposneBodyDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResposneBodyDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TrainingViewDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddTrainingAsync(TrainingCreateDTO training)
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

            var createdTraining = await _trainingService.AddTrainingAsync(userId, training);
            if (createdTraining is null)
            {
                return NotFound(new ErrorResposneBodyDTO
                {
                    DeveloperMessage = $"User with id: \"{userId}\" doesn't exist."
                });
            }

            return CreatedAtAction(nameof(GetTraining), new { trainingId = createdTraining.Id }, createdTraining);
        }

        /// <summary>
        /// Update training details.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/trainings/00000000-0000-0000-0000-000000000000
        ///     {
        ///         "date": "2020-11-11T00:00:00",
        ///         "exercises": [
        ///             {
        ///                 "id":"00000000-0000-0000-0000-000000000000",
        ///                 "name": "name",
        ///                 "sets": [
        ///                     {
        ///                         "id":"00000000-0000-0000-0000-000000000000"
        ///                         "weight": 111.1,
        ///                         "reps": 1
        ///                     }
        ///                 ]
        ///             }
        ///         ]
        ///     }
        ///
        /// </remarks>
        /// <param name="trainingId">Guid Id</param>
        /// <param name="training"></param>
        /// <returns></returns>
        /// <response code="400">If the authenticated user id from token is invalid format.</response>
        /// <response code="204">Training details updated.</response>
        /// <response code="404">If the authenticated user doesn't exist or user doesn's have desirable training.</response>
        /// <response code="409">If exercise or set Id/Ids in database are inconsistent.</response>
        [HttpPut("{trainingId}")]
        [ProducesResponseType(typeof(ErrorResposneBodyDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResposneBodyDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResposneBodyDTO), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateTraining(Guid trainingId, TrainingEditDTO training)
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

            var result = await _trainingService.UpdateTrainingAsync(userId, trainingId, training);

            return result switch
            {
                0 => NoContent(),
                1 => NotFound(new ErrorResposneBodyDTO { DeveloperMessage = $"User with id: \"{userId}\" doesn't exist." }),
                2 => NotFound(new ErrorResposneBodyDTO { DeveloperMessage = $"User doesn't have training with id: \"{trainingId}\"." }),
                3 => Conflict(new ErrorResposneBodyDTO { DeveloperMessage = $"Inconsistent Exercise or Set Id/Ids." }),
                _ => throw new NotImplementedException()
            };
        }

        /// <summary>
        /// Delete training.
        /// </summary>
        /// <param name="trainingId">Guid id</param>
        /// <returns></returns>
        /// <response code="400">If the authenticated user id from token is invalid format.</response>
        /// <response code="204">Training deleted.</response>
        [HttpDelete("{trainingId}")]
        [ProducesResponseType(typeof(ErrorResposneBodyDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTraining(Guid trainingId)
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

            await _trainingService.DeleteUserTrainingAsync(userId, trainingId);

            return NoContent();
        }
    }
}
