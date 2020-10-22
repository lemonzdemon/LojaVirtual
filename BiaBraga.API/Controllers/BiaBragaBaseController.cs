using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BiaBraga.API.Controllers
{

    public class BiaBragaBaseController : ControllerBase
    {
       // protected readonly ILogger<ControllerBase> _logger;

        public BiaBragaBaseController()
        {
            //_logger = logger;
        }

        protected IActionResult ErrorException(Exception exception, string method, int? id = null)
        {
           // string _id = id != null ? $", id: {id}" : string.Empty;

          //  _logger.LogError(exception, $"exception lancada metodo {method}{_id}");

            return StatusCode(500, "Ocorreu um erro interno com o tratamento dos dados.");
        }
    }
}
