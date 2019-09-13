using Exterminator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Exterminator.WebApi.Controllers
{
    [Route("api/logs")]
    public class LogController : Controller
    {
        ILogService _service;
        public LogController(ILogService logService)
        {
            _service = logService;
        }
        // TODO: Implement route which gets all logs from the ILogService, which should be injected through the constructor
        [Route("")]
        [HttpGet]
        public IActionResult GetAllLogs()
        {
            // return Ok(_service.GetAllLogs());
            return Ok(_service.GetAllLogs());
        }
    }
}