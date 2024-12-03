using System;

namespace tutorial_backend_dotnet.Domain.Dtos
{
    public class TutorialStepDto
    {
        public int StepId { get; set; }

        public int StepGroupId { get; set; } // Ensure this aligns with the schema
        
        public string StepAnchor { get; set; }

        public string StepTitle { get; set; }

        public string StepDescription { get; set; }

        public string StepPage { get; set; }

        public int StepOrder { get; set; }
        
        public bool IsRequiredAction { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
