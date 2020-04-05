using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxDataRead
{
    class ZipCodeData
    {
        public string zipCodeID;
        public double totalReturns;
        public int returnsAbove100k;
        public int returnsAbove200k;
        //public int AGI;

        public ZipCodeData()
        {
            zipCodeID = "";
            totalReturns = 0;
            returnsAbove100k = 0;
            returnsAbove200k = 0;
        }

        public string toString()
        {
            return zipCodeID + "," + totalReturns + "," + returnsAbove100k + "," + returnsAbove200k;
        }
    }
}
