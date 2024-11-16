using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tutorial_backend_dotnet.Models
{
    public class UserCompletedGroups
    {
        [Column("user_id")]
        public int UserId { get; set; }
        
        [Column("step_group_name")]
        public string StepGroupName { get; set; }
        
        [Column("completed_at")]
        public DateTime CompletedAt { get; set; }
        
        [Column("role_id")]
        public int RoleId { get; set; }
    }
}
