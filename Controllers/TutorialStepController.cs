using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using tutorial_backend_dotnet.Models;
using tutorial_backend_dotnet.Repositories;

namespace tutorial_backend_dotnet.Controllers
{
    [Route("api/tutorial-steps")]
    [ApiController]
    public class TutorialStepController : ControllerBase
    {
        private readonly ITutorialStepRepository _repository;

        public TutorialStepController(ITutorialStepRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TutorialStep>>>> GetAllActiveSteps()
        {
            var steps = await _repository.GetAllActiveSteps();
            return Ok(new ApiResponse<IEnumerable<TutorialStep>>
            {
                Status = "success",
                Message = "Tutorial steps retrieved successfully",
                Data = steps
            });
        }
        
        [HttpGet("role/{roleId:int}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ActiveTutorialStep>>>> GetStepsByRole(int roleId)
        {
            var steps = await _repository.GetStepsByRole(roleId);
            return Ok(new ApiResponse<IEnumerable<ActiveTutorialStep>>
            {
                Status = "success",
                Message = "Tutorial steps retrieved successfully",
                Data = steps
            });
        }
    }
}
