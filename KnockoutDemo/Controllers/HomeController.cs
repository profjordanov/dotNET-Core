using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using KnockoutDemo.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KnockoutDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        public IActionResult TabSelector()
        {
            return View();
        }

        public IActionResult KnockoutMvc()
        {
            return View();
        }

        [HttpPost]
        public IActionResult KnockoutMvc(SimpleTextViewModel model)
        {
            return View(model);

        }
    }
}
