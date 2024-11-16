using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace tutorial_backend_dotnet.Models
{
    public class UserTutorialProgress
    {
        [Column("user_id")]
        public int UserId { get; set; }
        
        [Column("role_id")]
        public int RoleId { get; set; }

        [Column("current_step_group_name")]
        public string CurrentStepGroupName { get; set; }
        
        [Column("current_step_id")]
        public int CurrentStepId { get; set; }

        [Column("is_complete")]
        public bool IsCompleted { get; set; }
        
        [Column("completed_at")]
        public DateTime CreatedAt { get; set; }
        
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
