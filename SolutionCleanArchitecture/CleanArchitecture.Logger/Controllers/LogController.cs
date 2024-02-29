using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Logger.Controllers
{
    [Route("Logs")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;

        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }
        // POST api/<LogController>
        [HttpPost]
        [Route("ObtenerError")]
        public void Post([FromBody] ErrorDto error)
        {
            _logger.LogError(error.Mensaje);
        }
    }
}
