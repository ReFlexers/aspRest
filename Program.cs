using asp.net_1.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "Cname"; 
        options.ExpireTimeSpan = TimeSpan.FromDays(1); 
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/AccessDenied"; 
    });

builder.Services.AddControllersWithViews();


string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

var app = builder.Build();

app.UseStaticFiles();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "reviews",
    pattern: "Reviews",
    defaults: new { controller = "Reviews", action = "Index" }
);


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "logout",
    pattern: "Logout",
    defaults: new { controller = "Logout", action = "Index" }
);

app.Run();
