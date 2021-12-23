using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TesteWebApp.Models;

namespace TesteWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }
        [Authorize(Roles = "TestWebApp.Usuario")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Adm() { return View(); }
        [Authorize]
        public IActionResult AdmUsuario() { return View(); }
        [Authorize]
        public IActionResult Usuario() { return View(); }
    }
}
