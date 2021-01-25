using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using GradDemo.Api.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GradDemo.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class cryptoController : ControllerBase
    {
        // GET: api/<cryptoController>
        [HttpGet("get-crypto/{id}&{currency}")]
        public Response<string> Get(string id, string currency)
        {
            string url = $"https://api.coingecko.com/api/v3/simple/price?ids={id}&vs_currencies={currency}";
            WebRequest request = WebRequest.Create($"{url}");
            WebResponse response = request.GetResponse();
            string responseFromServer;

            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(responseFromServer);
            }

            // Close the response.
            response.Close();

            return Response<string>.Successful(responseFromServer);
        }
    }
}
