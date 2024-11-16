using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tutorial_backend_dotnet.Models
{
    public class TutorialGroup
    {
        [Column("step_group_id")]
        public int StepGroupId { get; set; }
        
        [Column("role_id")]
        public int[] RoleId { get; set; }
        
        [Key]
        [Column("step_group_name")]
        public string StepGroupName { get; set; }
        
        [Column("step_group_description")]
        public string StepGroupDescription { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }
    }
}
