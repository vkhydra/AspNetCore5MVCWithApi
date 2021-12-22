using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Authorize]
        public IActionResult Index() { return View(); }
        [Authorize]
        public IActionResult Adm() { return View(); }
        [Authorize]
        public IActionResult AdmUsuario() { return View(); }
        [Authorize]
        public IActionResult Usuario() { return View(); }
    }
}
