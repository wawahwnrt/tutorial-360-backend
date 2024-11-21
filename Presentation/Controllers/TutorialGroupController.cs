using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tutorial_backend_dotnet.Application.Interfaces;
using tutorial_backend_dotnet.Domain.Dtos;
using tutorial_backend_dotnet.Models;
using tutorial_backend_dotnet.Utils;

namespace tutorial_backend_dotnet.Presentation.Controllers
{
    [ApiController]
    [Route("api/tutorial-groups")]
    public class TutorialGroupController : ControllerBase
    {
        private readonly ITutorialGroupService _service;

        public TutorialGroupController(ITutorialGroupService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves all active tutorial groups.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllActiveGroups()
        {
            var groups = await _service.GetAllActiveGroupsAsync();
            return Ok(new ApiResponse<IEnumerable<TutorialGroupDto>>
            {
                Status = "Success",
                Message = "Active tutorial groups retrieved successfully.",
                Data = groups
            });
        }

        /// <summary>
        /// Retrieves tutorial groups associated with a specific role.
        /// </summary>
        /// <param name="roleId">Role identifier.</param>
        [HttpGet("role/{roleId}")]
        public async Task<IActionResult> GetGroupsByRole(int roleId)
        {
            if (roleId <= 0)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Status = "Error",
                    Message = "Invalid role ID.",
                    Data = null
                });
            }

            var groups = await _service.GetGroupsByRoleAsync(roleId);
            return Ok(new ApiResponse<IEnumerable<TutorialGroupDto>>
            {
                Status = "Success",
                Message = $"Tutorial groups for role {roleId} retrieved successfully.",
                Data = groups
            });
        }

        /// <summary>
        /// Retrieves a specific tutorial group with its steps.
        /// </summary>
        /// <param name="groupId">Group identifier.</param>
        [HttpGet("{groupId}")]
        public async Task<IActionResult> GetGroupWithSteps(int groupId)
        {
            if (groupId <= 0)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Status = "Error",
                    Message = "Invalid group ID.",
                    Data = null
                });
            }

            var group = await _service.GetGroupWithStepsAsync(groupId);
            if (group == null)
            {
                return NotFound(new ApiResponse<TutorialGroupDto>
                {
                    Status = "Error",
                    Message = $"Tutorial group with ID {groupId} not found.",
                    Data = null
                });
            }

            return Ok(new ApiResponse<TutorialGroupDto>
            {
                Status = "Success",
                Message = $"Tutorial group with ID {groupId} retrieved successfully.",
                Data = group
            });
        }
    }
}
