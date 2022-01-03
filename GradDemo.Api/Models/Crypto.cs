using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradDemo.Api.Models
{
    public class Crypto
    {

        public class CryptoCurrency
        {
            public int Id { set; get; }
            public int Name { set; get; }
            public int Current_price { set; get; }

        }
    }
}
