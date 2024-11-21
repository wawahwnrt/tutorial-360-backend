using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tutorial_backend_dotnet.Application.Interfaces;
using tutorial_backend_dotnet.Domain.Entities;
using tutorial_backend_dotnet.Infrastructure.Data;

namespace tutorial_backend_dotnet.Infrastructure.Repositories
{
    public class TutorialStepRepository : BaseRepository<TutorialStep>, ITutorialStepRepository
    {
        public TutorialStepRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TutorialStep>> GetStepsByGroupAsync(int stepGroupId)
        {
            return await DbSet
                .Where(step => step.StepGroupId == stepGroupId && step.IsActive)
                .ToListAsync();
        }

        public async Task<TutorialStep> GetStepByIdAsync(int stepId)
        {
            return await DbSet.FirstOrDefaultAsync(s => s.StepId == stepId);
        }

        public async Task<IEnumerable<TutorialStep>> GetAllActiveStepsAsync()
        {
            return await DbSet
                .Where(s => s.IsActive)
                .OrderBy(s => s.StepOrder) // Active steps ordered for display
                .ToListAsync();
        }
    }
}
