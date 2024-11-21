using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using tutorial_backend_dotnet.Application.Interfaces;
using tutorial_backend_dotnet.Domain.Dtos;
using tutorial_backend_dotnet.Models;
using tutorial_backend_dotnet.Utils;

namespace tutorial_backend_dotnet.Presentation.Controllers
{
    [ApiController]
    [Route("api/user-progress")]
    public class UserTutorialProgressController : ControllerBase
    {
        private readonly IUserTutorialService _service;

        public UserTutorialProgressController(IUserTutorialService service)
        {
            _service = service;
        }
        
        /// <summary>
        /// Retrieves user tutorial progress.
        /// </summary>
        [HttpGet("{userId}/progress")]
        public async Task<IActionResult> GetUserProgressAsync(int userId)
        {
            var progress = await _service.GetUserProgressAsync(userId);
            return Ok(progress);
        }

        /// <summary>
        /// Retrieves all completed tutorials for a user and role.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="roleId">Role identifier.</param>
        /// <returns>A list of completed tutorials.</returns>
        [HttpGet("{userId}/role/{roleId}")]
        public async Task<IActionResult> GetCompletedTutorials(int userId, int roleId)
        {
            if (userId <= 0 || roleId <= 0)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Status = "Error",
                    Message = "Invalid user ID or role ID.",
                    Data = null
                });
            }

            var progress = await _service.GetCompletedTutorialsByUserAsync(userId, roleId);
            return Ok(new ApiResponse<IEnumerable<UserTutorialProgressDto>>
            {
                Status = "Success",
                Message = "Completed tutorials retrieved successfully.",
                Data = progress
            });
        }
        
        /// <summary>
        /// Marks a tutorial as completed for a user.
        /// </summary>
        [HttpPost("{userId}/complete")]
        public async Task<IActionResult> CompleteTutorialAsync(int userId, [FromBody] IEnumerable<UserTutorialProgressDto> tutorialProgress)
        {
            var result = await _service.CompleteTutorialAsync(userId, tutorialProgress);

            if (result)
            {
                return Ok(new ApiResponse<string>
                {
                    Status = "Success",
                    Message = "Tutorial marked as completed successfully.",
                    Data = null
                });
            }
            return BadRequest(new ApiResponse<string>
            {
                Status = "Error",
                Message = "Failed to mark tutorial as completed.",
                Data = null
            });
        }

        /// <summary>
        /// Resets progress for a user for a specific tutorial group.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>Confirmation of the operation.</returns>
        [HttpPost("{userId}/reset")]
        public async Task<IActionResult> ResetAllProgress(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Status = "Error",
                    Message = "Invalid user ID or group ID.",
                    Data = null
                });
            }

            await _service.ResetUserProgressAsync(userId);
            return Ok(new ApiResponse<string>
            {
                Status = "Success",
                Message = "User progress reset successfully.",
                Data = null
            });
        }
        
        /// <summary>
        ///     Resets a user's progress for a specific tutorial group.
        ///     This is useful when a user is removed from a group and their progress should be reset.
        /// </summary>
        ///
        /// <param name="userId">User identifier</param>
        ///     <param name="groupId">Tutorial group identifier</param>
        ///             <returns>Confirmation of the operation</returns>
        [HttpPost("{userId}/group/{groupId}/reset")]
        public async Task<IActionResult> ResetProgressByGroupAsync(int userId, int groupId)
        {
            if (userId <= 0 || groupId <= 0)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Status = "Error",
                    Message = "Invalid user ID or group ID.",
                    Data = null
                });
            }

            await _service.ResetUserProgressAsync(userId);
            return Ok(new ApiResponse<string>
            {
                Status = "Success",
                Message = "User progress reset successfully.",
                Data = null
            });
        }
        
    }
}
