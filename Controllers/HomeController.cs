using Microsoft.AspNetCore.Mvc;
using asp.net_1.Models;
using asp.net_1.ViewModels;

namespace asp.net_1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
          
            var viewModel = new UserViewModel();

            viewModel.Roles = GetRoles(); 

        
            return View(viewModel);
        }

        private List<Role> GetRoles()
        {
            
            var roles = new List<Role>(); 
                                          
            return roles; 
        }

    }
}
