using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using asp.net_1.Models;
using System;
using System.Linq;

namespace asp.net_1.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ApplicationContext _context;

        public ReviewsController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var reviews = _context.Reviews.ToList();
            return View(reviews);
        }

        [HttpPost]
        public IActionResult AddReview(Review review)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;

                var newReview = new Review
                {
                    UserName = userName,
                    Comment = review.Comment,
                    Date = DateTime.Now
                };

                _context.Reviews.Add(newReview);
                _context.SaveChanges();

                var reviews = _context.Reviews.ToList();
                return View("Index", reviews);
            }
            else
            {
                return RedirectToAction("Index", new { error = "User is not authenticated" });
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            var reviews = _context.Reviews.ToList();
            return View(reviews);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteReview(int id)
        {
            var review = _context.Reviews.Find(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
                return RedirectToAction("AdminPanel");
            }
            return View("AdminPanel", _context.Reviews.ToList());
        }
    }
}
