using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GradDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private static readonly HttpClient client;

        static CryptoController()
        {
            client = new HttpClient()
            {

            };
        }

        // GET: api/<CryptoController>
        [HttpGet("{id}/{vs_currency}")]
        public async Task<string> GetPrice(string id, string vs_currency)
        {
            string path = string.Format("https://api.coingecko.com/api/v3/simple/price?ids={0}&vs_currencies={1}", id, vs_currency);

            HttpResponseMessage response = await client.GetAsync(path);

            return await response.Content.ReadAsStringAsync();

        }

        // GET api/<CryptoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CryptoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CryptoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CryptoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
