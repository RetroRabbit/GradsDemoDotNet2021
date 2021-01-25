using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GradDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        static HttpClient client = new HttpClient();
        
        [HttpGet("GetCoinAndCur")]
        public async Task<string> GetCoin(string coinId, string currency)
        {
            string res = "";
            string url = $"https://api.coingecko.com/api/v3/simple/price?ids={coinId}&vs_currencies={currency}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                res = await response.Content.ReadAsStringAsync();
            }
            return res;
        }
    }
}
