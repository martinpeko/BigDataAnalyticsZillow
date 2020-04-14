﻿using System;
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
                entry.totalReturns = double.Parse(LineAsArray[1]);
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


            //Console.WriteLine(previousYearList.Count); //1624 entries
            //Console.WriteLine(recentYearList.Count); // 1619 entries
            //Console.ReadLine();
            // this is interesting because I would have thought that the recent list would have more zip codes being added
            

            // potential solutions to having mismatches in the zip code id's - 2 variables and you increment them separately. once one misses you recalibrate the variables?
            //or you could remove the entry in the list if it does not pair at all.

            for (int x = 0; x < recentYearList.Count; x++) // use recentYearList here because it will be longer than previousYearList due to added zip codes
            {
                if (previousYearList[x].zipCodeID != recentYearList[x].zipCodeID)
                {
                    Console.WriteLine("Discrepancy found between zip codes " + previousYearList[x].zipCodeID + " and " + recentYearList[x].zipCodeID);
                    //continue;
                    // solve this by re-searching from the current index onwards to find the missing zip
                    for(int y =0; y<recentYearList.Count; y++)
                    {
                        if(previousYearList[y].zipCodeID == recentYearList[x].zipCodeID)
                        {

                            /* NOTE TO SELF, MOVE THE WRITELINE TO A FUNCTION OR ELSE YOU WILL HAVE BIG CONFUSION MOVING FORWARD */
                        }
                    }
                }

                //// fixes the output of "infinity" percent increase
                //if (previousYearList[x].returnsAbove200k == 0)
                //{
                //    Console.WriteLine("Zip Code: " + previousYearList[x].zipCodeID + " | Net change in earners above $200k: +" + recentYearList[x].returnsAbove200k + " people. (started at zero)");
                //}
                //else if(recentYearList[x].returnsAbove200k == 0)
                //{
                //    Console.WriteLine("Zip Code: " + previousYearList[x].zipCodeID + " | Net change in earners above $200k: +" + recentYearList[x].returnsAbove200k + " people. (started at zero)");
                //}
                //else
                //{
                    percentDifference = ((double)(recentYearList[x].returnsAbove200k - previousYearList[x].returnsAbove200k) / previousYearList[x].returnsAbove200k);
                    Console.WriteLine("Zip Code: " + previousYearList[x].zipCodeID + " | Net change in earners above $200k: " + percentDifference.ToString("P", CultureInfo.InvariantCulture));
                //}
            }
        }

        static void Main(string[] args)
        {
            string filePath2011 = @"C:\Users\marti\source\repos\BigDataAnalyticsZillow\ProcessedInput\TX\2011.txt";
            string filePath2017 = @"C:\Users\marti\source\repos\BigDataAnalyticsZillow\ProcessedInput\TX\2017.txt";
            string filePathZHVI = @"C:\Users\marti\source\repos\BigDataAnalyticsZillow\Zip_Zhvi_AllHomeValues_NoHeaders.csv";

            List<ZipCodeData> TX2011data = new List<ZipCodeData>();
            List<ZipCodeData> TX2017data = new List<ZipCodeData>();


            import(filePath2011, TX2011data);
            import(filePath2017, TX2017data);

            printList(TX2011data);
            printList(TX2017data);

            analyze(TX2011data, TX2017data);

            Console.ReadLine();

            StreamReader homeValueReader = new StreamReader(filePathZHVI);

            while (!homeValueReader.EndOfStream)
            {
                ZipCodeHomeValue entry = new ZipCodeHomeValue();
                string line = homeValueReader.ReadLine();
                char token = ',';

                string[] LineAsArray = line.Split(token);
                entry.zipCode = LineAsArray[1];

                // many zip codes do not have data until a later date, for example some start in 2015
                if (LineAsArray[184] != "")
                {
                    entry.Price2011 = Double.Parse(LineAsArray[184]);
                }

                // keep in mind array space 184 is only January 2011, this program should take the 12 months of 2011 and average the price
                // by doing so you are also losing precision on the data, you may want to use that data too
                Console.WriteLine(entry.zipCode + " : " + entry.Price2011); 
                Console.ReadLine();
            }
        }
    }
}