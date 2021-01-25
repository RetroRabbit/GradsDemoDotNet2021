using GradDemo.Api.Entities;
using GradDemo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly ApplicationDbContext _context;

        public DemoController(ILogger<DemoController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
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
        public Response<string> SayHello([FromBody] string name)
        {
            var reallyOld = "Hello " + name;
            var lessOld = string.Format("Hello {0}", name);
            return Response<string>.Successful($"Hello, {name}!");
        }

        [HttpGet("should-fail")]
        public Response<string> ShouldFail()
        {
            return Response<string>.Error("Fails for test purposes");
        }

        [HttpPost("say-hello-to-more-people/{number}")]
        public Response<string> SayHelloToLotsOfPeople(int number, [FromBody] HelloRequest name)
        {
            return Response<string>.Successful($"[{number}] Hello {name.Name} and {name.OtherName} and especially you {name.LastName}");
        }

        [HttpPost("add-contact")]
        public async Task<Response<string>> AddContactAsync(ContactRequest contact)
        {
            _context.Contacts.Add(new Contact()
            {
                ContactNumber = contact.ContactNumber,
                LastName = contact.LastName,
                Name = contact.Name
            });

            await _context.SaveChangesAsync();

            return Response<string>.Successful("");
        }

        [HttpGet("contacts")]
        public async Task<Response<ICollection<Contact>>> GetContacts()
        {
            var contacts = await _context.Contacts.ToListAsync();
            return Response<ICollection<Contact>>.Successful(contacts);
        }
    }
}
