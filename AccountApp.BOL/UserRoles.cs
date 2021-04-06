using System.ComponentModel.DataAnnotations;

namespace AccountApp.BOL
{
    public class UserRoles
    {
        [Key]
        public int Id { get; set; }
        public string Role { get; set; }
        public string RoleName { get; set; }
    }
}
