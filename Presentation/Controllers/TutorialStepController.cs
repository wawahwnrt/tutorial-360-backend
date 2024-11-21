using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tutorial_backend_dotnet.Application.Interfaces;
using tutorial_backend_dotnet.Domain.Dtos;

namespace tutorial_backend_dotnet.Presentation.Controllers
{
    [ApiController]
    [Route("api/tutorial-steps")]
    public class TutorialStepController : ControllerBase
    {
        private readonly ITutorialStepService _service;

        public TutorialStepController(ITutorialStepService service)
        {
            _service = service;
        }

        /// <summary>
        ///     Retrieves all tutorial steps for a specific group.
        /// </summary>
        /// <param name="groupId">Group identifier.</param>
        /// <returns>A list of tutorial steps for the specified group.</returns>
        [HttpGet("group/{groupId}")]
        public async Task<IActionResult> GetStepsByGroupId(int groupId)
        {
            var steps = await _service.GetStepsByGroupIdAsync(groupId);
            return Ok(new ApiResponse<IEnumerable<TutorialStepDto>>
            {
                Status = "Success",
                Message = "Tutorial steps retrieved successfully.",
                Data = steps
            });
        }

        /// <summary>
        ///     Retrieves a specific tutorial step by ID.
        /// </summary>
        /// <param name="stepId">Step identifier.</param>
        /// <returns>The tutorial step if found.</returns>
        [HttpGet("{stepId}")]
        public async Task<IActionResult> GetStepById(int stepId)
        {
            var step = await _service.GetStepByIdAsync(stepId);
            return Ok(new ApiResponse<TutorialStepDto>
            {
                Status = "Success",
                Message = "Tutorial step retrieved successfully.",
                Data = step
            });
        }

        /// <summary>
        ///     Retrieves all active tutorial steps.
        /// </summary>
        /// <returns>A list of active tutorial steps.</returns>
        [HttpGet("active")]
        public async Task<IActionResult> GetAllActiveSteps()
        {
            var steps = await _service.GetAllActiveStepsAsync();
            return Ok(new ApiResponse<IEnumerable<TutorialStepDto>>
            {
                Status = "Success",
                Message = "Active tutorial steps retrieved successfully.",
                Data = steps
            });
        }
    }
}
