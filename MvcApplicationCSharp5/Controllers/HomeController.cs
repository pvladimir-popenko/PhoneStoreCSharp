using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcApplicationCSharp5.Models;

namespace MvcApplicationCSharp5.Controllers
{
    public class HomeController : Controller
    {
        //мапить - mapping 
        // ORM - object-relational mapping
        // Entity Framework Core
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Phones");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Hello([FromForm] string userName)
        {
            var user = new User() {Name = userName};
            return View(user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
