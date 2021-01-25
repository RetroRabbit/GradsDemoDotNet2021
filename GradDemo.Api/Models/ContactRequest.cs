using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradDemo.Api.Models
{
    public class ContactRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
    }
}
