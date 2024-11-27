using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tutorial_backend_dotnet.Domain.Entities
{
    [Table("USER_TUTORIAL_ROLE")]
    public class UserTutorialRole
    {
        [Key] [Column("role_id")] public int RoleId { get; set; }

        [Required]
        [Column("role_name")]
        [MaxLength(50)]
        public string RoleName { get; set; }

        // Navigation property
        public ICollection<TutorialGroupRole> TutorialGroupRoles { get; set; } = new List<TutorialGroupRole>();
    }
}
