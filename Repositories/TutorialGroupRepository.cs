using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tutorial_backend_dotnet.Data;
using tutorial_backend_dotnet.Models;

namespace tutorial_backend_dotnet.Repositories
{
    public class TutorialGroupRepository : ITutorialGroupRepository
    {
        private readonly AppDbContext _context;

        public TutorialGroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TutorialGroup>> GetAllActiveGroups()
        {
            return await _context.TutorialGroups.Where(g => g.IsActive).OrderBy(g => g.StepGroupId).ToListAsync();
        }

        public async Task<IEnumerable<ActiveTutorialGroup>> GetGroupsByRole(int roleId)
        {
            var groups = await GetAllActiveGroups();

            return (from @group in groups
                where @group.RoleId.Contains(roleId)
                select new ActiveTutorialGroup
                {
                    StepGroupId = @group.StepGroupId,
                    RoleId = @group.RoleId,
                    StepGroupName = @group.StepGroupName,
                    StepGroupDescription = @group.StepGroupDescription
                }).ToList();
        }
        
        public async Task<IEnumerable<ActiveTutorialGroupWithSteps>> GetAllActiveTutorials(int roleId)
        {
            var groups = await GetAllActiveGroups();
            var tutorialSteps = await new TutorialStepRepository(_context).GetStepsByRole(roleId);
            
            return (from @group in groups
                select new ActiveTutorialGroupWithSteps
                {
                    StepGroupId = @group.StepGroupId,
                    RoleId = @group.RoleId,
                    StepGroupName = @group.StepGroupName,
                    StepGroupDescription = @group.StepGroupDescription,
                    TutorialSteps = tutorialSteps.Where(step => step.StepGroupName == @group.StepGroupName).ToList()
                }).ToList();
        }
    }
}
