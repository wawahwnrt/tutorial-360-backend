using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tutorial_backend_dotnet.Application.Interfaces;
using tutorial_backend_dotnet.Domain.Dtos;

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
        ///     Retrieves user tutorial progress.
        /// </summary>
        [HttpGet("{userId}/progress")]
        public async Task<IActionResult> GetUserProgressAsync(int userId)
        {
            var progress = await _service.GetUserProgressAsync(userId);
            var userTutorialProgressDtos = progress.ToList();
            if (userTutorialProgressDtos.Any())
            {
                return Ok(new ApiResponse<IEnumerable<UserTutorialProgressDto>>
                {
                    Status = "Success",
                    Message = "User progress retrieved successfully.",
                    Data = userTutorialProgressDtos
                });
            }
            
            return NotFound(new ApiResponse<string?>
            {
                Status = "Error",
                Message = "No progress found for the specified user."
            });
        }

        /// <summary>
        ///     Retrieves all completed tutorials for a user and role.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="roleId">Role identifier.</param>
        /// <returns>A list of completed tutorials.</returns>
        [HttpGet("{userId}/role/{roleId}")]
        public async Task<IActionResult> GetCompletedTutorials(int userId, int roleId)
        {
            if (userId <= 0 || roleId <= 0)
            {
                return BadRequest(new ApiResponse<string?>
                {
                    Status = "Error",
                    Message = "Invalid user ID or role ID."
                });
            }

            var progress = await _service.GetCompletedTutorialsByUserAsync(userId, roleId);
            var userTutorialProgressDtos = progress.ToList();
            if (userTutorialProgressDtos.Any())
            {
                return Ok(new ApiResponse<IEnumerable<UserTutorialProgressDto>>
                {
                    Status = "Success",
                    Message = "Completed tutorials retrieved successfully.",
                    Data = userTutorialProgressDtos
                });
            }
            
            return NotFound(new ApiResponse<string?>
            {
                Status = "Error",
                Message = "No completed tutorials found for the specified user and role."
            });
        }

        /// <summary>
        ///     Marks a tutorial as completed for a user.
        /// </summary>
        [HttpPost("{userId}/complete")]
        public async Task<IActionResult> CompleteTutorialAsync(int userId,
            [FromBody] IEnumerable<UserTutorialProgressDto> tutorialProgress)
        {
            var result = await _service.CompleteTutorialAsync(userId, tutorialProgress);

            if (result)
            {
                return Ok(new ApiResponse<string?>
                {
                    Status = "Success",
                    Message = "Tutorial marked as completed successfully."
                });
            }

            return BadRequest(new ApiResponse<string?>
            {
                Status = "Error",
                Message = "Failed to mark tutorial as completed."
            });
        }

        /// <summary>
        ///     Resets progress for a user for a specific tutorial group.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>Confirmation of the operation.</returns>
        [HttpPost("{userId}/reset")]
        public async Task<IActionResult> ResetAllProgress(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest(new ApiResponse<string?>
                {
                    Status = "Error",
                    Message = "Invalid user ID."
                });
            }

            await _service.ResetUserProgressAsync(userId);
            return Ok(new ApiResponse<string?>
            {
                Status = "Success",
                Message = "User progress reset successfully."
            });
        }

        /// <summary>
        ///     Resets a user's progress for a specific tutorial group.
        ///     This is useful when a user is removed from a group and their progress should be reset.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="groupId">Tutorial group identifier</param>
        /// <returns>Confirmation of the operation</returns>
        [HttpPost("{userId}/group/{groupId}/reset")]
        public async Task<IActionResult> ResetProgressByGroupAsync(int userId, int groupId)
        {
            if (userId <= 0 || groupId <= 0)
            {
                return BadRequest(new ApiResponse<string?>
                {
                    Status = "Error",
                    Message = "Invalid user ID or group ID."
                });
            }

            await _service.ResetProgressByGroupAsync(userId, groupId);
            return Ok(new ApiResponse<string?>
            {
                Status = "Success",
                Message = "Tutorial group id: " + groupId + " progress reset successfully."
            });
        }

        /// <summary>
        ///     Retrieves the last completed step for a user.
        ///     <param name="userId">User identifier</param>
        ///     <param name="roleId">Role identifier</param>
        ///     <returns>UserTutorialProgressDto entity</returns>
        ///     <returns>Null if no progress is found</returns>
        ///     <returns>Null if the user has not completed any steps</returns>
        ///     <returns>Null if the user has not completed any steps for the specified role</returns>
        /// </summary>
        [HttpGet("{userId:int}/role/{roleId:int}/continue")]
        public async Task<IActionResult> GetUserLastCompletedStepAsync(int userId, int roleId)
        {
            var progress = await _service.GetUserLastCompletedStepAsync(userId, roleId);
            if (progress.Equals(null))
            {
                return NotFound(new ApiResponse<string?>
                {
                    Status = "Error",
                    Message = "No progress found for the specified user and role."
                });
            }
            return Ok(new ApiResponse<UserTutorialProgressDto>
            {
                Status = "Success",
                Message = "Last completed step retrieved successfully.",
                Data = progress
            });
        }
    }
}
