using Logging.Controllers;
using System.Reflection;

namespace Logging
{
    public class WeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly ILogger<WeatherForecastService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WeatherForecastService(ILogger<WeatherForecastService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public WeatherForecast[] GetWeatherForecasts()
        {
            var weatherForecasts = Enumerable.Range(1, 2).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            string methodName = nameof(GetWeatherForecasts);
            string smeTraceId = HttpContextHelper.GetSmeTraceId(_httpContextAccessor.HttpContext);
            _logger.LogInfo(smeTraceId, methodName, "WeatherForecasts fetched successfully.");

            return weatherForecasts;
        }
    }
}
