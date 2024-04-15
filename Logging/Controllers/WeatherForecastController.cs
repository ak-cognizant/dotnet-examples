using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;

namespace Logging.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherForecastService _weatherForecastService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
            _logger.LogInformation($"Is Logger Enabled: {LoggerExtension.IsEnabled}");
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            string methodName = nameof(Get);
            string smeTraceId = this.GetSmeTraceId();

            
            var weatherForecasts = _weatherForecastService.GetWeatherForecasts();

            
            // Logging Information
            _logger.LogInfo(smeTraceId, methodName, "WeatherForecasts fetched successfully.");

            // Logging API Request
            _logger.LogRequest(smeTraceId, methodName, "some/another/api/call", weatherForecasts);

            // Logging API Response
            _logger.LogResponse(smeTraceId, methodName, "some/another/api/call", HttpStatusCode.NotFound, null);
            _logger.LogResponse(smeTraceId, methodName, "some/another/api/call", HttpStatusCode.OK, weatherForecasts);

            return weatherForecasts;
        }
    }
}
