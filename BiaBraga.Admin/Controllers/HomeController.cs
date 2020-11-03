using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BiaBraga.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using BiaBraga.Admin.Services;
using BiaBraga.Domain.Enums;
using BiaBraga.Repository.Classes;

namespace BiaBraga.Admin.Controllers
{
    [AuthService(Role.Administrador, Role.Supervisor)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var pass = Encript.HashValue("123fsdfsd&*()!sdhfladgsdfgdfsgfdfdsf456");

            //6ED5833CF35286EBF8662B7B5949F0D742BBEC3F
            //9F6DD00E09464F9799BC35ACBE48EDC2B8D28CE8
            //34781457DD87161F51D36426090F5196E7102D6E

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [AllowAnonymous]
        public IActionResult Restrito()
        {
            return View();
        }
    }
}
