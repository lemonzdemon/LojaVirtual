using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BiaBraga.Admin.Controllers
{
    [Route("shared/acess")]
    public class AcessoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("Restrito")]
        public IActionResult Restrito()
        {
            return View();
        }
    }
}
