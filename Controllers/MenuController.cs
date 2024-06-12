using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using asp.net_1.ViewModels;
using asp.net_1.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace asp.net_1.Controllers
{
 
    public class MenuController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MenuController(ApplicationContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var menuItems = _context.MenuItems.ToList();
            return View(menuItems);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            var menuItems = _context.MenuItems.ToList();
            return View(menuItems);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddMenuItem(MenuItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Image.CopyTo(fileStream);
                    }
                }

                var menuItem = new MenuItem
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    ImageUrl = "/Images/" + uniqueFileName
                };

                _context.MenuItems.Add(menuItem);
                _context.SaveChanges();

                return RedirectToAction("AdminPanel");
            }

            return View("AdminPanel", _context.MenuItems.ToList());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteMenuItem(int id)
        {
            var menuItem = _context.MenuItems.Find(id);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
                _context.SaveChanges();
                return RedirectToAction("AdminPanel");
            }
            return View("AdminPanel", _context.MenuItems.ToList());
        }
    }
}
