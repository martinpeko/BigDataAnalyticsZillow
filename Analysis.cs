using System;
using System.IO;
using System.Threading.Tasks;
using Zillow.Services.Schema;
using System.Collections.Generic;
using System.Globalization;

namespace Zillow.Services
{
    class Analysis
    {
        public static void import(string filePath, List<ZipCodeData> list)
        {
            StreamReader fileReader = new StreamReader(filePath);
            while (!fileReader.EndOfStream)
            {
                string line = fileReader.ReadLine();

                char token = ',';

                string[] LineAsArray = line.Split(token);


                ZipCodeData entry = new ZipCodeData();
                entry.zipCodeID = LineAsArray[0];
                entry.population = double.Parse(LineAsArray[1]);
                entry.returnsAbove100k = int.Parse(LineAsArray[2]);
                entry.returnsAbove200k = int.Parse(LineAsArray[3]);
                list.Add(entry);
            }
        }

        public static void printList(List<ZipCodeData> list)
        {
            foreach(var entry in list)
            {
                Console.WriteLine(entry.toString());
            }
        }

        public static void analyze(List<ZipCodeData> previousYearList, List<ZipCodeData> recentYearList)
        {
            double percentDifference;
            for (int x = 0; x < recentYearList.Count; x++)
            {
                if (previousYearList[x].zipCodeID != recentYearList[x].zipCodeID)
                {
                    Console.WriteLine("Discrepancy found between zip codes" + previousYearList[x].zipCodeID + " and " + recentYearList[x].zipCodeID);
                    continue;
                }
                percentDifference = ((double)(recentYearList[x].returnsAbove200k - previousYearList[x].returnsAbove200k) / previousYearList[x].returnsAbove200k);
                Console.WriteLine("Zip Code: " + previousYearList[x].zipCodeID + " Net change in earners above $200k: " + percentDifference.ToString("P", CultureInfo.InvariantCulture));
            }
        }

        static void Main(string[] args)
        {
            string filePath2011 = @"C:\Users\marti\source\repos\BigDataAnalyticsZillow\2011testdata.txt";
            string filePath2017 = @"C:\Users\marti\source\repos\BigDataAnalyticsZillow\2017testdata.txt";

            List<ZipCodeData> TX2011data = new List<ZipCodeData>();
            List<ZipCodeData> TX2017data = new List<ZipCodeData>();


            import(filePath2011, TX2011data);
            import(filePath2017, TX2017data);

            printList(TX2011data);
            printList(TX2017data);

            analyze(TX2011data, TX2017data);

            Console.ReadLine();
        }
    }
}