using System.Collections.Generic;

namespace asp.net_1.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public List<UserModel> Users { get; set; } = new List<UserModel>();
    }
}
