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
using GradDemo.Api.Providers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GradDemo.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private readonly CoinGeckoProvider _coinGeckoProvider;

        public CryptoController(CoinGeckoProvider coinProv)
        {
            _coinGeckoProvider = coinProv;
        }

        [HttpGet("value/for/{coinId}/currency/{currency}")]
        public async Task<Response<CryptoCoinResponse>> GetCoin(string coinId, string currency)
        {
            var result = new CryptoCoinResponse();

            var res = await _coinGeckoProvider.GetValueForCoin(coinId, currency);

            if (res.HasValue)
            {
                return Response<CryptoCoinResponse>.Successful(new CryptoCoinResponse()
                {
                    Value = res.Value
                });
            }

            return Response<CryptoCoinResponse>.Error("Something went wrong");
        }
    }
}
