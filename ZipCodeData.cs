using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zillow.Services
{
    class ZipCodeData
    {
        public string zipCodeID;
        public double population;
        public int returnsAbove100k;
        public int returnsAbove200k;
        //public int AGI;

        public ZipCodeData()
        {

        }

        public string toString()
        {
            return zipCodeID + "," + population + "," + returnsAbove100k + "," + returnsAbove200k;
        }


    }
}
