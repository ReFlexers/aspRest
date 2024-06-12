using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using asp.net_1.Models;
using asp.net_1.ViewModels;

namespace asp.net_1.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationContext _context;

        public RegisterController(ApplicationContext context)
        {
            _context = context;
        }

 
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(UserModel model)
        {
            if (_context.Users.Any(u => u.Username == model.Username))
            {
                ModelState.AddModelError("Username", "Это имя пользователя уже занято.");
                return View("Index", new UserViewModel { UserModel = model });
            }

            model.RoleName = "User";

            _context.Users.Add(model);
            _context.SaveChanges();

         
            return RedirectToAction("Index", "Login");
        }
    }
}
