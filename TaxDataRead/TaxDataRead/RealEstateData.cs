using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxDataRead
{
    class RealEstateData
    {
        public string zipcodeID;
        public int avgValue;

        public RealEstateData()
        {
            zipcodeID = "";
            avgValue = 0;
        }

        public string toString()
        {
            return zipcodeID + "," + avgValue;
        }
    }
}
