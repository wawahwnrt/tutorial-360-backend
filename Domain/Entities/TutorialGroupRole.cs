using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tutorial_backend_dotnet.Domain.Entities
{
    [Table("tutorial_group_roles")]
    public class TutorialGroupRole
    {
        [Key, Column("step_group_id", Order = 0)]
        public int StepGroupId { get; set; }

        [Key, Column("role_id", Order = 1)]
        public int RoleId { get; set; }

        // Navigation property
        public UserTutorialRole UserTutorialRole { get; set; }
    }
}
