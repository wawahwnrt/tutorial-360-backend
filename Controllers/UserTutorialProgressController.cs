using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tutorial_backend_dotnet.Models;
using tutorial_backend_dotnet.Repositories;

namespace tutorial_backend_dotnet.Controllers
{
    [Route("api/user-tutorial")]
    [ApiController]
    public class UserTutorialProgressController: ControllerBase
    {
        private readonly IUserTutorialProgressRepository _repository;
        
        public UserTutorialProgressController(IUserTutorialProgressRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public async Task<ActionResult<ApiResponse<bool>>> CheckIfUserNewToTutorial(int userId, int roleId)
        {
            var isNew = await _repository.CheckIfUserNewToTutorial(userId, roleId);
            return Ok(new ApiResponse<bool>
            {
                Status = "success",
                Message = "User tutorial status of user ID " + userId + " and role ID " + roleId + " retrieved successfully",
                Data = isNew
            });
        }
        
        [HttpGet("{userId:int}/{roleId:int}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserCompletedTutorials>>>> GetUserCompletedTutorials(int userId, int roleId)
        {
            var tutorials = await _repository.GetUserCompletedTutorials(userId, roleId);
            return Ok(new ApiResponse<IEnumerable<UserCompletedTutorials>>
            {
                Status = "success",
                Message = "User completed tutorials of user ID " + userId + " and role ID " + roleId + " retrieved successfully",
                Data = tutorials
            });
        }
        
        [HttpGet("{userId:int}/role/{roleId:int}/status")]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserTutorialStatus>>>> GetUserTutorialStatus(int userId, int roleId)
        {
            var status = await _repository.GetUserTutorialStatus(userId, roleId);
            return Ok(new ApiResponse<IEnumerable<UserTutorialStatus>>
            {
                Status = "success",
                Message = "User tutorial status of user ID " + userId + " and role ID " + roleId + " retrieved successfully",
                Data = status
            });
        }
        
        [HttpGet("{userId:int}/role/{roleId:int}/progress")]
        public async Task<ActionResult<ApiResponse<UserTutorialProgress>>> GetLatestUserTutorialProgress(int userId, int roleId)
        {
            var progress = await _repository.GetLatestUserTutorialProgress(userId, roleId);
            return Ok(new ApiResponse<UserTutorialProgress>
            {
                Status = "success",
                Message = "Latest progress of user ID " + userId + " and role ID " + roleId + " retrieved successfully",
                Data = progress
            });
        }
        
        [HttpPost]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserTutorialStatus>>>> AddUserCompletedTutorials(IEnumerable<UserCompletedTutorials> tutorials)
        {
            var status = await _repository.AddUserCompletedTutorials(tutorials);
            return Ok(new ApiResponse<IEnumerable<UserTutorialStatus>>
            {
                Status = "success",
                Message = "Completed Tutorials added successfully",
                Data = status
            });
        }
        
        [HttpPut("{userId:int}/role/{roleId:int}/reset/{groupName}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserTutorialStatus>>>> ResetUserCompletedTutorials(int userId, int roleId, string groupName)
        {
            var status = await _repository.ResetUserCompletedTutorials(userId, roleId, groupName);
            return Ok(new ApiResponse<IEnumerable<UserTutorialStatus>>
            {
                Status = "success",
                Message = "User completed tutorials reset successfully",
                Data = status
            });
        }
    }
}
