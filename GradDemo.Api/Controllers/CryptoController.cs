using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using GradDemo.Api.Models;
using GradDemo.Api.Providers;
using Microsoft.AspNetCore.Identity;
using GradDemo.Api.Entities;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GradDemo.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private readonly CoinGeckoProvider _coinGeckoProvider;
        private readonly UserManager<Device> _userManager;
        public CryptoController(CoinGeckoProvider coinProv, UserManager<Device> userManager)
        {
            _coinGeckoProvider = coinProv;
            _userManager = userManager;
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
        [Authorize]
        [HttpGet("custom/response/")]
        public async Task<Response<CryptoCoinResponse>> GetCoinCustom()
        {
            var user = await _userManager.GetUserAsync(User);
            string currency = user.currency;
            if (currency == null)
            {
                currency = "usd";
            }
            var res = await _coinGeckoProvider.GetValueForCoin($"{currency}");
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
