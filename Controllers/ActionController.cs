using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using asp.net_1.ViewModels;
using asp.net_1.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace asp.net_1.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class ActionController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ActionController(ApplicationContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var actions = _context.Actions.Select(a => new ActionViewModel
            {
                Title = a.Title,
                Description = a.Description,
                Details = a.Details,
                ImageUrl = a.ImageUrl
            }).ToList();

            return View(actions);
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult AdminPanel()
        {
            var actions = _context.Actions.ToList();
            return View(actions);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult AddAction(ActionFViewModel model)
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

                var action = new Models.Action
                {
                    Title = model.Title,
                    Description = model.Description,
                    Details = model.Details,
                    ImageUrl = "/Images/" + uniqueFileName
                };

                _context.Actions.Add(action);
                _context.SaveChanges();

                return RedirectToAction("AdminPanel");
            }

            return View("AdminPanel", _context.Actions.ToList());
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult DeleteAction(int id)
        {
            var action = _context.Actions.Find(id);
            if (action != null)
            {
                _context.Actions.Remove(action);
                _context.SaveChanges();
                return RedirectToAction("AdminPanel");
            }
            return View("AdminPanel", _context.Actions.ToList());
        }
    }
}
