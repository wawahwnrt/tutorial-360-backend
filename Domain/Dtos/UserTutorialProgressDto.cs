using System;

namespace tutorial_backend_dotnet.Domain.Dtos
{
    public class UserTutorialProgressDto
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int StepId { get; set; }
        public int StepGroupId { get; set; } // Added to align with schema
        public DateTime CompletedAt { get; set; }
        public string StepGroupName { get; set; }
    }
}
