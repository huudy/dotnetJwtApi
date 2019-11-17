using System.ComponentModel.DataAnnotations.Schema;

namespace myJWTAPI.Core.Models
{
    // [Table("UserRoles")]
    public class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}