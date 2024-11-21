using System.Collections.Generic;
using System.Threading.Tasks;
using tutorial_backend_dotnet.Domain.Dtos;

namespace tutorial_backend_dotnet.Application.Interfaces
{
    public interface ITutorialStepService
    {
        /// <summary>
        ///     Retrieves all active tutorial steps.
        /// </summary>
        Task<IEnumerable<TutorialStepDto>> GetAllActiveStepsAsync();

        /// <summary>
        ///     Retrieves steps for a specific group.
        /// </summary>
        /// <param name="groupId">Group identifier</param>
        Task<IEnumerable<TutorialStepDto>> GetStepsByGroupIdAsync(int groupId);

        /// <summary>
        ///     Retrieves a specific step by its ID.
        /// </summary>
        /// <param name="stepId">Step identifier</param>
        Task<TutorialStepDto> GetStepByIdAsync(int stepId);
    }
}
