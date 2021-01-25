using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using static GradDemo.Api.Models.Crypto;
using GradDemo.Api.Models;
using System.IO;

namespace GradDemo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CryptoController: ControllerBase
    {

        [HttpGet("get-crypto/{id}&{currency}")]
        public Response<string> Get(string id, string currency)
        {
            string url = $"https://api.coingecko.com/api/v3/simple/price?ids={id}&vs_currencies={currency}"; //url of where to get data from
            WebRequest request = WebRequest.Create($"{url}");
            WebResponse response = request.GetResponse();
            string priceFromCoinGecko; // string that will read data

            using (Stream dataStreamfromSite = response.GetResponseStream())
            {
                StreamReader streamReader = new StreamReader(dataStreamfromSite);
                priceFromCoinGecko = streamReader.ReadToEnd();
            }
            response.Close(); //closes whatever response you would've gotten

            return Response<string>.Successful(priceFromCoinGecko); // "prints" value on page 
        }
    }
}
