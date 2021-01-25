using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GradDemo.Api.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GradDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        HttpClient _client;
        HttpClient client
        {
            get 
            {
                if (_client == null)
                {
                    _client =  new HttpClient();
                }

                return _client;
            }
        }

        // GET: api/<CryptoController>
        [HttpGet]
        public async Task<IEnumerable<CryptoCurrency>> Get()
        {
            string path = @"https://api.coingecko.com/api/v3/coins/list";
            HttpResponseMessage response = await client.GetAsync(path);

            string apiResponse = await response.Content.ReadAsStringAsync();
            var priceList = JsonConvert.DeserializeObject<List<CryptoCurrency>>(apiResponse);

            return priceList;
        }


        [HttpGet("{id}/{currency_sym}")]
        public async Task<string> GetPrice(string id,string currency_sym)
        {
            string path =  string.Format("https://api.coingecko.com/api/v3/simple/price?ids={0}&vs_currencies={1}", id, currency_sym) ;
            HttpResponseMessage response = await client.GetAsync(path);

            string apiResponse = await response.Content.ReadAsStringAsync();
            //var price = JsonConvert.DeserializeObject<CryptoCurrency>(apiResponse);

            return apiResponse;
        }


        // GET api/<CryptoController>/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            return null;
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
