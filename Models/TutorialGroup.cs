using System;
using System.Collections.Generic;
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
        
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
    
    public class ActiveTutorialGroup : TutorialGroup
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
    }
    
    public class ActiveTutorialGroupWithSteps : TutorialGroup
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
        
        public List<ActiveTutorialStep> TutorialSteps { get; set; }
    }
    
    public class TutorialGroupStatus : TutorialGroup
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
        
        public bool IsCompleted { get; set; }
    }
    
    public class UserTutorialStatus : TutorialGroup
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
        
        public bool IsCompleted { get; set; }
        
        public List<TutorialStepStatus> TutorialSteps { get; set; }
    }
}
