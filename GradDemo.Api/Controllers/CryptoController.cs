using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace GradDemo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CryptoController: ControllerBase
    {

        private static HttpClient client;

        static CryptoController()
        {
            client = new HttpClient() { };
        }

        [HttpGet("get-crypto/{id}&{currency}")]
        public async Task<string> GetPrice(string id, string currency)
        {
            string url = $"https://api.coingecko.com/api/v3/simple/price?ids={id}&vs_currencies={currency}"; //url of where to get data from

            HttpResponseMessage responseMessage = await client.GetAsync(url);

            return await responseMessage.Content.ReadAsStringAsync();
        }
    }
}
