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
using BiaBraga.Repository.Interfaces;
using BiaBraga.Domain.Models.Entitys;

namespace BiaBraga.Admin.Controllers
{
    [AuthService(Role.Administrador, Role.Supervisor)]
    public class HomeController : Controller
    {
        private readonly IBiaBragaRepository _repository;

        public HomeController(IBiaBragaRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public async Task<IActionResult> Contact(DateTime? minDate, DateTime? maxDate)
        {
            var contacts = await _repository.GetAllAsync<Contact>();

            DateTime dateInit = minDate ?? DateTime.Now.AddMonths(-1);
            DateTime dateFinish = !maxDate.HasValue || maxDate.Value > DateTime.Now ? DateTime.Now : maxDate.Value;

            ViewData["minDate"] = dateInit.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = dateFinish.ToString("yyyy-MM-dd");

            ViewData["qntImportante"] = contacts.Count(x => x.Important);
            ViewData["qntNovo"] = contacts.Count(x => x.New);
            ViewData["qntTotal"] = contacts.Count();

            return View(contacts.Where(x => (x.Date >= dateInit && x.Date <= dateFinish) || x.Important).OrderBy(x => x.Important).ThenBy(x => x.New));
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
