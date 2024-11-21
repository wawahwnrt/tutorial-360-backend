using System.Collections.Generic;
using System.Threading.Tasks;
using tutorial_backend_dotnet.Domain.Entities;

namespace tutorial_backend_dotnet.Application.Interfaces
{
    public interface ITutorialStepRepository
    {
        /// <summary>
        /// Retrieves all tutorial steps for a specific group.
        /// </summary>
        /// <param name="groupId">Group identifier.</param>
        /// <returns>A list of tutorial steps belonging to the specified group.</returns>
        Task<IEnumerable<TutorialStep>> GetStepsByGroupAsync(int stepGroupId);

        /// <summary>
        /// Retrieves a specific tutorial step by ID.
        /// </summary>
        /// <param name="stepId">Step identifier.</param>
        /// <returns>The tutorial step if found; otherwise, null.</returns>
        Task<TutorialStep> GetStepByIdAsync(int stepId);

        /// <summary>
        /// Retrieves all active tutorial steps.
        /// </summary>
        /// <returns>A list of active tutorial steps.</returns>
        Task<IEnumerable<TutorialStep>> GetAllActiveStepsAsync();
    }
}
