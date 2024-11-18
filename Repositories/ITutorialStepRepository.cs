using System.Collections.Generic;
using System.Threading.Tasks;
using tutorial_backend_dotnet.Models;

namespace tutorial_backend_dotnet.Repositories
{
    public interface ITutorialStepRepository
    {
        Task<IEnumerable<TutorialStep>> GetAllActiveSteps();
        Task<IEnumerable<ActiveTutorialStep>> GetStepsByRole(int roleId);
    }
}
