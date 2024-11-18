using System.Collections.Generic;
using System.Threading.Tasks;
using tutorial_backend_dotnet.Models;

namespace tutorial_backend_dotnet.Repositories
{
    public interface IUserTutorialProgressRepository
    {
        Task<bool> CheckIfUserNewToTutorial(int userId, int roleId);
        Task<IEnumerable<UserCompletedTutorials>> GetUserCompletedTutorials(int userId, int roleId);
        Task<IEnumerable<UserTutorialStatus>> GetUserTutorialStatus(int userId, int roleId);
        Task<UserTutorialProgress> GetLatestUserTutorialProgress(int userId, int roleId);
        Task<IEnumerable<UserTutorialStatus>> AddUserCompletedTutorials(IEnumerable<UserCompletedTutorials> tutorials);
        Task<IEnumerable<UserTutorialStatus>> ResetUserCompletedTutorials(int userId, int roleId, string groupName);
    }
}


