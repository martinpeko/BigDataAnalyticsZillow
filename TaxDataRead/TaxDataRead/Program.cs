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
            string filePath = @"C:\Users\Parox\Desktop\bigdata\TXtaxData\2017.xlsx";
            string output = @"C:\Users\Parox\Documents\GitHub\BigDataAnalyticsZillow\ProcessedInput\TX\2017.txt";

            if (File.Exists(filePath))
            {
                Console.WriteLine("File Found.");
            }
            else
            {
                Console.WriteLine("Error. File not found.");
            }

            // testing the class
            Excel excel = new Excel(filePath, 1);

            //starting to assemble a method to reading the important parts of the tax data
            //List<ZipCodeData> taxData = new List<ZipCodeData>();
            List<string> allZipcodes = new List<string>();
            int x = 14;
            int y = 0;

            while (excel.readCell(x, y) != 99999)
            {
                //Console.WriteLine("Reading...");
                ZipCodeData entry = new ZipCodeData();
                entry.zipCodeID = excel.readCell(x, y).ToString();

                y += 2; // moves over 2 columns
                entry.totalReturns = (int)excel.readCell(x, y);

                x += 5;
                entry.returnsAbove100k = (int)excel.readCell(x, y);

                x += 1;
                entry.returnsAbove200k = (int)excel.readCell(x, y);

                allZipcodes.Add(entry.zipCodeID + ", " +
                                entry.totalReturns.ToString() + ", " +
                                entry.returnsAbove100k.ToString() + ", " +
                                entry.returnsAbove200k.ToString());

                // moves down 2 rows and resets to first colomn to get next zipcode
                y = 0;
                x += 2;
                Console.WriteLine(x);
            }
            File.WriteAllLines(output, allZipcodes);
            Console.WriteLine("Finished.");
            Console.ReadLine(); // pause
        }
    }
}
