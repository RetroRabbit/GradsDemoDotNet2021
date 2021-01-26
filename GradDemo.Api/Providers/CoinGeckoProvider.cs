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
        string currUrl;
        List<string> currencies = new List<string>();

        public CoinGeckoProvider(string baseUrl,string currenciesurl)
        {
            client.BaseAddress = new Uri(baseUrl);
            currUrl = currenciesurl;
        }
        public async Task<List<string>> GetCurrencies()
        {
            HttpResponseMessage response = await client.GetAsync(currUrl);
            List<string> curr = new List<string>();
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                string[] splitter = res.Split("\"");
                foreach (var item in splitter)
                {
                    if (item!= "," && item!= "]" && item!="[")
                    {
                        curr.Add(item.Trim());
                    }
                }
            }
            return curr;
        }
        public async Task<double?> GetValueForCoin(string currency)
        {
            double? resultValue = null;
            currencies =  await GetCurrencies();

            string url = $"api/v3/simple/price?ids=bitcoin&vs_currencies={currency}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                var coinGeckoResult = JsonConvert.DeserializeObject(res);
                string val;
                var someResult = res;
                string[] splitter = someResult.Split(":");
                val = splitter[2].Substring(0, splitter[2].Length-2);
                if (val == null)
                {
                    return null;
                }

                return double.Parse(val);
            }

            return null;
        }
    }
}
