using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace tutorial_backend_dotnet.Models
{
    public class UserCompletedTutorials
    {
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }
        
        [Column("step_group_name")]
        public string StepGroupName { get; set; }
        
        [Column("step_id")]
        public int StepId { get; set; }

        [Column("completed_at")]
        public DateTime CompletedAt { get; set; }
    }
    
    public class UserTutorialProgress
    {
        public int UserId { get; set; }
        
        public int RoleId { get; set; }

        public string CurrentStepGroupName { get; set; }
        
        public int CurrentStepId { get; set; }
        
        public bool IsCompleteAll { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
