using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using tutorial_backend_dotnet.Models;
using tutorial_backend_dotnet.Repositories;

namespace tutorial_backend_dotnet.Controllers
{
    [Route("api/tutorial-groups")]
    [ApiController]
    public class TutorialGroupController : ControllerBase
    {
        private readonly ITutorialGroupRepository _repository;

        public TutorialGroupController(ITutorialGroupRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TutorialGroup>>>> GetAllActiveGroups()
        {
            var groups = await _repository.GetAllActiveGroups();
            return Ok(new ApiResponse<IEnumerable<TutorialGroup>>
            {
                Status = "success",
                Message = "Tutorial groups retrieved successfully",
                Data = groups
            });
        }
        
        [HttpGet("role/{roleId:int}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ActiveTutorialGroup>>>> GetGroupsByRole(int roleId)
        {
            var groups = await _repository.GetGroupsByRole(roleId);
            return Ok(new ApiResponse<IEnumerable<ActiveTutorialGroup>>
            {
                Status = "success",
                Message = "Tutorial groups retrieved successfully",
                Data = groups
            });
        }
        
        [HttpGet("role/{roleId:int}/tutorials")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ActiveTutorialGroupWithSteps>>>> GetAllActiveTutorials(int roleId)
        {
            var groups = await _repository.GetAllActiveTutorials(roleId);
            return Ok(new ApiResponse<IEnumerable<ActiveTutorialGroupWithSteps>>
            {
                Status = "success",
                Message = "Tutorial groups retrieved successfully",
                Data = groups
            });
        }
    }
}
