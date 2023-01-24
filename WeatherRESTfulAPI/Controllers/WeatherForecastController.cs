using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using WeatherRESTfulAPI.DTOs;

namespace WeatherRESTfulAPI.Controllers
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<WeatherForecast> Get()
        {
            string login = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.GivenName)?.Value;

            if (login is null)
            {
                throw new Exception("Invalid login");
            }
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}