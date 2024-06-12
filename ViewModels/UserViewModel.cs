
using asp.net_1.Models;

namespace asp.net_1.ViewModels
{
    public class UserViewModel
    {
        public UserModel UserModel { get; set; }
        public UserModel RegisterModel { get; set; } 
        public List<Role> Roles { get; set; } 
    }
}
