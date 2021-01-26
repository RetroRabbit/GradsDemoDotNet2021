using GradDemo.Api.Models.CoinGecko;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GradDemo.Api.Providers
{
    public class CoinGeckoProvider
    {
        static HttpClient client = new HttpClient();

        public async Task<double?> GetValueForCoin(string coinId, string currency)
        {
            double? resultValue = null;

            string url = $"https://api.coingecko.com/api/v3/simple/price?ids={coinId}&vs_currencies={currency}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();

                var coinGeckoResult = JsonConvert.DeserializeObject<CoinPrice>(res);

                if (currency.Equals("zar", StringComparison.InvariantCultureIgnoreCase))
                {
                    resultValue = coinGeckoResult.bitcoin.zar;
                }
                else if (currency.Equals("usd", StringComparison.InvariantCultureIgnoreCase))
                {
                    resultValue = coinGeckoResult.bitcoin.usd;
                }

                return resultValue;
            }

            return null;
        }
    }
}
