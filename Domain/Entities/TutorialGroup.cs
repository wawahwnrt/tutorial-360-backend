using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tutorial_backend_dotnet.Domain.Entities
{
    [Table("tutorial_group")]
    public class TutorialGroup
    {
        [Key] [Column("step_group_id")] public int StepGroupId { get; set; }

        [Column("step_group_name", TypeName = "text")]
        [Required]
        [MaxLength(255)]
        public string StepGroupName { get; set; }

        [Column("step_group_description", TypeName = "text")]
        public string StepGroupDescription { get; set; }

        [Column("is_active")] public bool IsActive { get; set; }

        [Column("created_at", TypeName = "timestamp")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at", TypeName = "timestamp")]
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public ICollection<TutorialGroupRole> TutorialGroupRoles { get; set; } = new List<TutorialGroupRole>();

        public ICollection<TutorialStep> TutorialSteps { get; set; } = new List<TutorialStep>();
    }
}
