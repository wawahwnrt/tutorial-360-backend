using System.Collections.Generic;
using System.Threading.Tasks;
using tutorial_backend_dotnet.Domain.Dtos;

namespace tutorial_backend_dotnet.Application.Interfaces
{
    public interface IUserTutorialService
    {
        /// <summary>
        ///     Retrieves a user's tutorial progress.
        /// </summary>
        /// <param name="userId">User identifier</param>
        Task<IEnumerable<UserTutorialProgressDto>> GetUserProgressAsync(int userId);

        /// <summary>
        ///     Retrieves the last completed step for a user.
        ///     <param name="userId">User identifier</param>
        ///     <param name="roleId">Role identifier</param>
        ///     <returns>UserTutorialProgressDto entity</returns>
        ///     <returns>Null if no progress is found</returns>
        ///     <returns>Null if the user has not completed any steps</returns>
        ///     <returns>Null if the user has not completed any steps for the specified role</returns>
        /// </summary>
        Task<UserTutorialProgressDto> GetUserLastCompletedStepAsync(int userId, int roleId);

        /// <summary>
        ///     Marks a tutorial as completed for a user.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="tutorialProgress">Tutorial progress DTO</param>
        Task<bool> CompleteTutorialAsync(int userId, IEnumerable<UserTutorialProgressDto> tutorialProgress);

        /// <summary>
        ///     Resets a user's tutorial progress.
        /// </summary>
        /// <param name="userId">User identifier</param>
        Task<bool> ResetUserProgressAsync(int userId);

        /// <summary>
        ///     Retrieves all completed tutorials for a user and role.
        /// </summary>
        Task<IEnumerable<UserTutorialProgressDto>> GetCompletedTutorialsByUserAsync(int userId, int roleId);

        /// <summary>
        ///     Resets a user's progress for a specific tutorial group.
        ///     This is useful when a user is removed from a group and their progress should be reset
        ///     for that group.
        /// </summary>
        Task ResetProgressByGroupAsync(int userId, int groupId);
    }
}
