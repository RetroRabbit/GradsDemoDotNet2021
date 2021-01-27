using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradDemo.Api.Entities
{
    public class Device : IdentityUser
    {
        internal string name;

        public string currency { get; set; }
    }
}
