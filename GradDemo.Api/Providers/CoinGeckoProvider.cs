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

        public CoinGeckoProvider(string baseUrl)
        {
            client.BaseAddress = new Uri(baseUrl);
        }

        public async Task<double?> GetValueForCoin(string coinId, string currency)
        {
            double? resultValue = null;

            string url = $"api/v3/simple/price?ids={coinId}&vs_currencies={currency}";
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
        public async Task<string[]> GetCurrency()
        {
            string[] currencyResults = null;
            string path = $"/api/v3/simple/supported_vs_currencies";

            HttpResponseMessage currencyResponse = await client.GetAsync(path);

            if (currencyResponse.IsSuccessStatusCode)
            {
                var result = await currencyResponse.Content.ReadAsStringAsync();
                var currencyResult = JsonConvert.DeserializeObject<string[]>(result);
                currencyResults = currencyResult;
                return currencyResults;
            }
            return null;
        }
    }
}
