using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zillow.Services
{
    class ZipCodeHomeValue
    {
        public string zipCode;
        public double Price2011;

        public ZipCodeHomeValue()
        {
            zipCode = "";
            Price2011 = 0;
        }
    }
}
