using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using tutorial_backend_dotnet.Models;
using tutorial_backend_dotnet.Repositories;

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
    public async Task<ActionResult<IEnumerable<TutorialGroup>>> GetAll()
    {
        return Ok(await _repository.GetAllTutorialGroups());
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<TutorialGroup>> GetByName(string name)
    {
        var group = await _repository.GetTutorialGroupByName(name);
        if (group == null) return NotFound();
        return Ok(group);
    }   
    
    [HttpPost]
    public async Task<IActionResult> Create(TutorialGroup group)
    {
        await _repository.AddTutorialGroup(group);
        return CreatedAtAction(nameof(GetByName), new { id = group.StepGroupId }, group);
    }

    [HttpPut("{name}")]
    public async Task<IActionResult> Update(string name, TutorialGroup group)
    {
        if (name != group.StepGroupName) return BadRequest();
        await _repository.UpdateTutorialGroup(group);
        return NoContent();
    }

    [HttpDelete("{name}")]
    public async Task<IActionResult> Delete(string name)
    {
        await _repository.DeleteTutorialGroup(name);
        return NoContent();
    }
}
