using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradDemo.Api.Models.CoinGecko
{
    public class CoinPrice
    {
        public Bitcoin bitcoin { get; set; }
    }

    public class Bitcoin
    {
        public int zar { get; set; }
        public int usd { get; set; }
    }
}
