using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BiaBraga.Config.Models;
using System.IO;

namespace BiaBraga.Config.Controllers
{
    public class ConfigController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void Index(ConfigFormViewModel model)
        {
            var fileConfig = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.FullName, "config.txt");

            using StreamWriter sw = new StreamWriter(fileConfig);
            sw.Write($"{model.ConnectionString}<!split!>{model.SecretKey}<!split!>");
            sw.Close();
            Program.Shutdown();
        }
    }
}
