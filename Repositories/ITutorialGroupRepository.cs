using System.Collections.Generic;
using System.Threading.Tasks;
using tutorial_backend_dotnet.Models;

namespace tutorial_backend_dotnet.Repositories
{
    public interface ITutorialGroupRepository
    {
        Task<IEnumerable<TutorialGroup>> GetAllTutorialGroups();
        Task<TutorialGroup> GetTutorialGroupByName(string name);
        Task AddTutorialGroup(TutorialGroup group);
        Task UpdateTutorialGroup(TutorialGroup group);
        Task DeleteTutorialGroup(string name);
    }
}
