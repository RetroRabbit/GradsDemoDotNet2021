using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GradDemo.Api.Models;
using Newtonsoft.Json;
using GradDemo.Api.Models.CoinGecko;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GradDemo.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        static HttpClient client = new HttpClient();

        [HttpGet("value/for/{coinId}/currency/{currency}")]
        public async Task<Response<CryptoCoinResponse>> GetCoin(string coinId, string currency)
        {
            var result = new CryptoCoinResponse();

            string res = "";
            string url = $"https://api.coingecko.com/api/v3/simple/price?ids={coinId}&vs_currencies={currency}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                res = await response.Content.ReadAsStringAsync();

                var coinGeckoResult = JsonConvert.DeserializeObject<CoinPrice>(res);

                if (currency.Equals("zar", StringComparison.InvariantCultureIgnoreCase))
                {
                    result.Value = coinGeckoResult.bitcoin.zar;
                }
                else if (currency.Equals("usd", StringComparison.InvariantCultureIgnoreCase))
                {
                    result.Value = coinGeckoResult.bitcoin.usd;
                }

                return Response<CryptoCoinResponse>.Successful(result);
            }

            return Response<CryptoCoinResponse>.Error("Something went wrong");
        }
    }
}
