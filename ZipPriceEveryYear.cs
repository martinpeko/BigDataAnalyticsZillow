using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zillow.Services
{
    class ZipPriceEveryYear
    {
        public string zipCode;
        public double price2011;
        public double price2012;
        public double price2013;
        public double price2014;
        public double price2015;
        public double price2016;
        public double price2017;

        public double percentIncrease()
        {
            double percentIncrease = 0;

            percentIncrease = price2017 - price2011;

            percentIncrease = percentIncrease / price2011;

            percentIncrease *= 100;

            return percentIncrease;
        }
    }
}
