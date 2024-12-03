using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tutorial_backend_dotnet.Domain.Entities
{
    public class TutorialStep
    {
        [Key] [Column("step_id")] public int StepId { get; set; }

        [Column("step_group_id")] public int StepGroupId { get; set; }

        [Column("step_group_name")] public string StepGroupName { get; set; }
        
        [Column("step_anchor")] public string StepAnchor { get; set; }

        [Column("step_title")] public string StepTitle { get; set; }

        [Column("step_description")] public string StepDescription { get; set; }

        [Column("step_page")] public string StepPage { get; set; }

        [Column("step_order")] public int StepOrder { get; set; }
        
        [Column("is_required_action")] public bool IsRequiredAction { get; set; }

        [Column("is_active")] public bool IsActive { get; set; }

        [Column("created_at")] public DateTime CreatedAt { get; set; }

        [Column("updated_at")] public DateTime UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey("StepGroupId")] public TutorialGroup TutorialGroup { get; set; }
    }
}
