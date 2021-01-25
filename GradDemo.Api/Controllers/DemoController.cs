using GradDemo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradDemo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<DemoController> _logger;

        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("say-hello")]
        public string SayHello([FromBody] string name)
        {
            var reallyOld = "Hello " + name;
            var lessOld = string.Format("Hello {0}", name);

            return $"Hello {name}";
        }

        [HttpPost("say-hello-to-more-people/{number}")]
        public string SayHelloToLotsOfPeople(int number, [FromBody] HelloRequest name)
        {
            return $"[{number}] Hello {name.Name} and {name.OtherName} and especially you {name.LastName}";
        }
    }
}
