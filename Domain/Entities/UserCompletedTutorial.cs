using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tutorial_backend_dotnet.Domain.Entities
{
    public class UserCompletedTutorial
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }

        [Column("step_id")]
        public int StepId { get; set; }

        [Column("step_group_id")]
        public int StepGroupId { get; set; }

        [Column("step_group_name")]
        public string StepGroupName { get; set; }

        [Column("complete_at")]
        public DateTime CompletedAt { get; set; }

        // Navigation properties
        [ForeignKey("StepId")]
        public TutorialStep TutorialStep { get; set; }

        [ForeignKey("StepGroupId")]
        public TutorialGroup TutorialGroup { get; set; }
    }
}
