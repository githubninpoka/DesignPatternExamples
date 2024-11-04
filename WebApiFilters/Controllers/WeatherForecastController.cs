using Microsoft.AspNetCore.Mvc;
using WebApiFilters.Filters.ActionFilters;
using WebApiFilters.Filters.ResourceFilters;
using WebApiFilters.Filters.ResultFilters;

namespace WebApiFilters.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [TypeFilter(typeof(WeatherForecastActionFilterAttribute))]

        [TypeFilter(typeof(WeatherForecastResultFilterAttribute))]

        [TypeFilter(typeof(WeatherForecastResourceFilterAttribute))]

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("This is ran inside the action.");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
