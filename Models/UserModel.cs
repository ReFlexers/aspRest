using System.ComponentModel.DataAnnotations;

namespace asp.net_1.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string RoleName { get; set; }

        public bool RememberMe { get; set; } 
    }
}
