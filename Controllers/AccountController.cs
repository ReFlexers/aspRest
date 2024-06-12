using asp.net_1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class AccountController : Controller
{
    public IActionResult Account()
    {

        var username = User.Identity.Name; 
        var role = User.FindFirst(ClaimTypes.Role)?.Value; 
        ViewBag.Username = username;
        ViewBag.Role = role;


        return View();
    }
}