using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KennUwareHR.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KennUwareHR.Controllers
{
    public class LoginController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Authentication viewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }
            return View(viewModel);
        }

    }
}