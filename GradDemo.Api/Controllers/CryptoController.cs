using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GradDemo.Api.Controllers
{
    public class CryptoController
    {
        [HttpGet("cryptoShit")]
        public string GetCrypto() {
            string outp = "";

            WebRequest request = WebRequest.Create(
             "https://api.coingecko.com/api/v3/simple/price?ids=bitcoin&vs_currencies=USD");
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;

            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                outp = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(outp);
            }

            // Close the response.
            response.Close();

            return outp;
        }

    }
}
