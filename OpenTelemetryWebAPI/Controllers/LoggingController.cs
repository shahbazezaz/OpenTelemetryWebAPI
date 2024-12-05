using Microsoft.AspNetCore.Mvc;

namespace OpenTelemetryWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoggingController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;
        public LoggingController(ILogger<LoggingController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService; 
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogDebug($"This is a {LogLevel.Debug} message");
            _logger.LogInformation($"{LogLevel.Information} Information");
            _logger.LogError(new Exception("Application exception"), "Exception");

            _userService.Login("shahbaz", "password007");
            return Ok();
        }
    }
}
