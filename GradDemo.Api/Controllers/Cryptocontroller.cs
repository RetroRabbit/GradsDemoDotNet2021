using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GradDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cryptocontroller : ControllerBase
    {
        // GET: api/<Cryptocontroller>
        [HttpGet]
        public IEnumerable<string> Get(string id)
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Cryptocontroller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        // GET api/<Cryptocontroller>/5
        [HttpGet("BitcoinPrice")]
        public string GetCryptoPrice()
        {
            string responseFromServer = "";

            WebRequest request = WebRequest.Create("https://api.coingecko.com/api/v3/simple/price?ids=bitcoin&vs_currencies=ZAR");

            request.Credentials = CredentialCache.DefaultCredentials;

            WebResponse response = request.GetResponse();

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

            return responseFromServer;
        }
        // POST api/<Cryptocontroller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Cryptocontroller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Cryptocontroller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
