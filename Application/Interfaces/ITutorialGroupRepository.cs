using System.Collections.Generic;
using System.Threading.Tasks;
using tutorial_backend_dotnet.Domain.Entities;

namespace tutorial_backend_dotnet.Application.Interfaces
{
    public interface ITutorialGroupRepository
    {
        /// <summary>
        ///     Retrieves all active tutorial groups with their associated steps.
        /// </summary>
        /// <returns>A list of active tutorial groups.</returns>
        Task<IEnumerable<TutorialGroup>> GetAllActiveGroupsAsync();

        /// <summary>
        ///     Retrieves tutorial groups filtered by a specific role.
        /// </summary>
        /// <param name="roleId">Role identifier.</param>
        /// <returns>A list of tutorial groups associated with the role.</returns>
        Task<IEnumerable<TutorialGroup>> GetGroupsByRoleAsync(int roleId);

        /// <summary>
        ///     Retrieves a specific tutorial group along with its steps.
        /// </summary>
        /// <param name="groupId">Group identifier.</param>
        /// <returns>The tutorial group with its associated steps, or null if not found.</returns>
        Task<TutorialGroup> GetGroupWithStepsAsync(int groupId);
        
        /// <summary>
        ///     Retrieves all active tutorial steps for a specific role.
        /// </summary>
        ///     <param name="roleId">Role identifier.</param>
        /// <returns>A list of active tutorial steps for the specified role.</returns>
        Task<IEnumerable<TutorialStep>> GetActiveStepsByRoleAsync(int roleId);
    }
}
