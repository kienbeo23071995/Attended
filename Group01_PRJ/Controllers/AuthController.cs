using Group01_PRJ.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Group01_PRJ.Controllers
{
    public class AuthController : Controller
    {
        private readonly AttendedContext _context;

        public AuthController(AttendedContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User userParam)
        {
            User user = null;
            if (ModelState.IsValid)
            {
                user = _context.Users
                    .Include(u => u.UserGroups)
                        .ThenInclude(g => g.Group)
                    .FirstOrDefault(item => (item.Username == userParam.Username && item.Password == userParam.Password));
                if (user != null)
                {
                    var jsonSerializerSettings = new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    };
                    var json = JsonConvert.SerializeObject(user, jsonSerializerSettings);
                    HttpContext.Session.SetString("user", json);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Dont have that user!";
                }
            }
            return View(user);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Login", "Auth");
        }
    }
}
