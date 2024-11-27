using System.Collections.Generic;
using System.Threading.Tasks;
using tutorial_backend_dotnet.Domain.Dtos;

namespace tutorial_backend_dotnet.Application.Interfaces
{
    public interface ITutorialGroupService
    {
        /// <summary>
        ///     Retrieves all active tutorial groups.
        /// </summary>
        Task<IEnumerable<TutorialGroupDto>> GetAllActiveGroupsAsync();

        /// <summary>
        ///     Retrieves tutorial groups associated with a specific role.
        /// </summary>
        /// <param name="roleId">Role identifier</param>
        Task<IEnumerable<TutorialGroupDto>> GetGroupsByRoleAsync(int roleId);

        /// <summary>
        ///     Retrieves a single tutorial group with its steps.
        /// </summary>
        /// <param name="groupId">Group identifier</param>
        Task<TutorialGroupDto> GetGroupWithStepsAsync(int groupId);
        
        /// <summary>
        ///     Retrieves all active tutorial steps for a specific role.
        /// </summary>
        /// <param name="roleId">Role identifier</param>
        Task<IEnumerable<TutorialStepDto>> GetActiveStepsByRoleAsync(int roleId);
    }
}
