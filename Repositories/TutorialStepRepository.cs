using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tutorial_backend_dotnet.Data;
using tutorial_backend_dotnet.Models;

namespace tutorial_backend_dotnet.Repositories
{
    public class TutorialStepRepository : ITutorialStepRepository
    {
        private readonly AppDbContext _context;

        public TutorialStepRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TutorialStep>> GetAllActiveSteps()
        {
            return await _context.TutorialSteps
                .Where(step => step.IsActive)
                .OrderBy(step => step.StepOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<ActiveTutorialStep>> GetStepsByRole(int roleId)
        {
            var steps = await GetAllActiveSteps();
            
            return steps
                .Where(step => step.RoleId == roleId)
                .Select(step => new ActiveTutorialStep
                {
                    StepId = step.StepId,
                    RoleId = step.RoleId,
                    StepName = step.StepName,
                    StepDescription = step.StepDescription,
                    StepOrder = step.StepOrder
                })
                .ToList();
        }
    }
}
