using System.Collections.Generic;
using System.Threading.Tasks;
using tutorial_backend_dotnet.Models;

namespace tutorial_backend_dotnet.Repositories
{
    public interface ITutorialGroupRepository
    {
        Task<IEnumerable<TutorialGroup>> GetAllActiveGroups();
        Task<IEnumerable<ActiveTutorialGroup>> GetGroupsByRole(int roleId);
        Task<IEnumerable<ActiveTutorialGroupWithSteps>> GetAllActiveTutorials(int roleId);
    }
}
