using System.Collections.Generic;

namespace tutorial_backend_dotnet.Domain.Dtos
{
    public class TutorialGroupDto
    {
        public int StepGroupId { get; set; }

        public string StepGroupName { get; set; }

        public string StepGroupDescription { get; set; }

        public bool IsActive { get; set; }

        public List<UserTutorialRoleDto> RoleIds { get; set; } =
            new List<UserTutorialRoleDto>(); // Default initialization

        public List<TutorialStepDto> TutorialSteps { get; set; } =
            new List<TutorialStepDto>(); // Default initialization
    }
}
