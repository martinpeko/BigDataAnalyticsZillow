using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxDataRead
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter '1' to process tax data OR '2' to process real estate data...");
            string input = "";
            input = Console.ReadLine();
            if (input == "1") taxDataProcess();
            else if (input == "2") realEstateProcess();
            else Console.WriteLine("Error: Invalid Input");
            Console.ReadLine();
        }

        static void taxDataProcess()
        {
            int docsProcessed = 0;
            while (docsProcessed <= 6) // reads through and processes all tax data from 2011-2017 per state
            {
                string[] input = { "2011.xls", "2012.xls", "2013.xls", "2014.xls", "2015.xls", "2016.xls", "2017.xlsx" };
                string[] output = { "2011.txt", "2012.txt", "2013.txt", "2014.txt", "2015.txt", "2016.txt", "2017.txt", };
                string filePath = @"C:\Users\Parox\Desktop\bigdata\taxdata\AK\%inputFileName%".Replace("%inputFileName%", input[docsProcessed]);
                string writePath = @"C:\Users\Parox\Documents\GitHub\BigDataAnalyticsZillow\ProcessedInput\AK\%outputFileName%".Replace("%outputFileName%", output[docsProcessed]);

                if (File.Exists(filePath))
                {
                    Console.WriteLine(input[docsProcessed] + " Found.");
                }
                else
                {
                    Console.WriteLine("Error. " + input[docsProcessed] + " not found.");
                }

                // testing the class
                Excel excel = new Excel(filePath, 1);

                //creating a list of strings to store processed zipcode data
                List<string> allZipcodes = new List<string>();
                int x = 14;
                int y = 0;

                while (excel.readCell(x, y) != 99999)
                {
                    //Console.WriteLine("Reading...");
                    ZipCodeData entry = new ZipCodeData();
                    entry.zipCodeID = excel.readCell(x, y).ToString();

                    y += 2; // moves over 2 columns (gets total # of return for zipcode)
                    entry.totalReturns = (int)excel.readCell(x, y);

                    x += 5; // moves down 5 rows (gets returns filed above $100k)
                    entry.returnsAbove100k = (int)excel.readCell(x, y);

                    x += 1; // moves down 1 row (gets returns filed above $200k)
                    entry.returnsAbove200k = (int)excel.readCell(x, y);

                    // adds processed info per zipcode to output file
                    allZipcodes.Add(entry.zipCodeID + "," +
                                    entry.totalReturns.ToString() + "," +
                                    entry.returnsAbove100k.ToString() + "," +
                                    entry.returnsAbove200k.ToString());

                    // moves down 2 rows and resets to first colomn to get next zipcode
                    y = 0;
                    x += 2;
                    Console.WriteLine(x); // Debug line, prints row of excel spreadsheet to locate errors
                }
                File.WriteAllLines(writePath, allZipcodes);
                Console.WriteLine("Finished " + input[docsProcessed] + ".");
                ++docsProcessed;
            }
        }

        static void realEstateProcess()
        {
            string inputPath = @"C:\Users\Parox\Documents\GitHub\BigDataAnalyticsZillow\TaxDataRead\RealEstateTest.xlsx";
            string outputPath = @"C:\Users\Parox\Documents\GitHub\BigDataAnalyticsZillow\TaxDataRead\RealEstateTX.txt";

            if (File.Exists(inputPath))
            {
                Console.WriteLine(inputPath + " Found.");
            }
            else
            {
                Console.WriteLine("Error. " + inputPath + " not found.");
            }

            Excel excel = new Excel(inputPath, 1);
            List<string> realEstate = new List<string>(); // list to house all zipcodes and yearly averge real estate worth

            int x = 2;
            int y = 2;
            int yearEndMarker = 1;
            string allYearAvg = "";

            while (excel.readCell(x, y) != 99999)
            {
                RealEstateData data = new RealEstateData();
                data.zipcodeID = excel.readCell(x, y).ToString(); // gets the zipcode we are currently on

                y = 8;
                while(y <= 91) // 91 is colomn ID of december 2017
                {
                    Console.WriteLine(x + "," + y); //DEBUG
                    data.avgValue = data.avgValue + (int)excel.readCell(x, y);

                    if(yearEndMarker == 12) // when a year ended
                    {
                        data.avgValue = data.avgValue / 12; // get the average real estate price for the year
                        allYearAvg = allYearAvg + "," + data.avgValue.ToString(); // add our yearly average to the all year average string (holds all years 2011-2017)
                        data.avgValue = 0; // reset the average price for next year
                        yearEndMarker = 0; // reset the year
                    }
                    ++y; // go to the next month
                    ++yearEndMarker; // keeps track of when a year is over
                }

                realEstate.Add(data.zipcodeID + allYearAvg.ToString()); // adds line to list with zipcode and 2011s - 2017s average real estate price
                ++x; // incrementing the row we are on, and resetting y to the zipcode colomn
                y = 2;

                File.WriteAllLines(outputPath, realEstate);
                Console.WriteLine("Finished " + inputPath + ".");
            }
        }
    }
}
