using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BiaBraga.Admin.Controllers
{
    public class AcessosController1 : Controller
    {
        public IActionResult Restrito()
        {
            return View();
        }
    }
}
