using BiaBraga.API.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace BiaBraga.API.Services
{
    public class ControllerService : ControllerBase
    {
        protected readonly ILogger<ControllerBase> _logger;

        public ControllerService(ILogger<ControllerBase> logger)
        {
            _logger = logger;
        }

        protected IActionResult ErrorException(Exception exception, string method, int? id = null)
        {
            string _id = id != null ? $", id: {id}" : string.Empty;

            _logger.LogError(exception, $"exception lancada metodo {method}{_id}");

            return StatusCode(500, "Ocorreu um erro interno com o tratamento dos dados.");
        }

        protected void NewLog(string method, TypeLogger typeLogger, string message = null)
        {
            string _message = string.IsNullOrEmpty(message) ? string.Empty : $", {message}";
            switch (typeLogger)
            {
                case TypeLogger.StartProcess:
                    _logger.LogInformation($"Iniciando metodo {method}{_message}");
                    break;
                case TypeLogger.FinishSucess:
                    _logger.LogInformation($"Metodo {method} finalizado com sucesso{_message}");
                    break;
                case TypeLogger.FinishDivergence:
                    _logger.LogWarning($"Metodo {method} finalizado com divergência{_message}");
                    break;
                case TypeLogger.Other:
                    _logger.LogInformation($"Metodo {method}{_message}");
                    break;
                case TypeLogger.StartMapping:
                    _logger.LogInformation($"Metodo {method}, iniciando mapeamento{_message}");
                    break;
            }
        }
    }
}
