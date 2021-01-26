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
        User user = new User();
        public CryptoController(CoinGeckoProvider coinProv)
        {
            _coinGeckoProvider = coinProv;
        }

        [HttpGet("value/for/bitcoin/currency/{currency}")]
        public async Task<Response<CryptoCoinResponse>> GetCoin(string currency)
        {
            var result = new CryptoCoinResponse();
            var res = await _coinGeckoProvider.GetValueForCoin(currency);

            if (res.HasValue)
            {
                return Response<CryptoCoinResponse>.Successful(new CryptoCoinResponse()
                {
                    Value = res.Value
                });
            }

            return Response<CryptoCoinResponse>.Error("Something went wrong");
        }
        [HttpGet("currencies/")]
        public async Task<Response<List<string>>> GetCurrencies()
        {

            var res = await _coinGeckoProvider.GetCurrencies();

            if (res.Count>0)
            {
                return Response<List<string>>.Successful(res);
            }

            return Response<List<string>>.Error("Something went wrong");
        }
        [HttpGet("custom/response/for/{name}")]
        public async Task<Response<CryptoCoinResponse>> GetCoinCustom(string name)
        {
            string currency;
            if (name == user.name)
            {
                 currency = user.currency;
            }
            
            var result = new CryptoCoinResponse();
            var res = await _coinGeckoProvider.GetValueForCoin("usd");

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
