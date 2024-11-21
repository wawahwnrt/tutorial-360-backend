using System.Collections.Generic;
using System.Threading.Tasks;
using tutorial_backend_dotnet.Domain.Entities;

namespace tutorial_backend_dotnet.Application.Interfaces
{
    public interface IUserTutorialProgressRepository
    {
        /// <summary>
        /// Retrieves a user's tutorial progress.
        /// </summary>
        Task<IEnumerable<UserCompletedTutorial>> GetUserProgressAsync(int userId);

        /// <summary>
        /// Marks a tutorial as completed for a user.
        /// </summary>
        Task<bool> MarkTutorialsAsCompletedAsync(IEnumerable<UserCompletedTutorial> tutorialProgress);

        /// <summary>
        /// Resets a user's tutorial progress.
        /// </summary>
        Task<bool> ResetUserProgressAsync(int userId);
        
        /// <summary>
        ///     Resets a user's progress for a specific tutorial group.
        ///     This is useful when a user is removed from a group and their progress should be reset.
        /// </summary>
        ///     <param name="userId">User identifier</param>
        ///     <param name="groupId">Tutorial group identifier</param>
        Task ResetProgressByGroupAsync(int userId, int groupId);
    }
}
