using System;
using System.IO;
using System.Threading.Tasks;
using Zillow.Services.Schema;
using System.Collections.Generic;


namespace Zillow.Services
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            ZillowClient client = new ZillowClient("X1-ZWz17aws2gte6j_7x5fi");
            Task<searchresults> search = client.GetSearchResultsAsync("14736 Munnberry Oval", "Newbury OH 44065");

            
            foreach (SimpleProperty prop in search.Result.response.results)
            {

                var zest = client.GetZestimateAsync(prop.zpid.ToString());

                var c = client.GetChartAsync(prop.zpid.ToString(), "dollar", "600", "300");

                var rc = client.GetRegionChartAsync("", "", "", prop.address.zipcode, "dollar", "600", "300", SimpleChartDuration.Item1year, ChartVariant.detailed);

                var comp = client.GetCompsAsync(prop.zpid.ToString(), "10");

                Task.WaitAll(zest, c, rc, comp);

                Console.WriteLine(string.Format("Regional : {0}", rc.Result.response.url));

                Console.WriteLine(string.Format("{0}, {1} - {2} {3}", prop.address.latitude, prop.address.longitude, zest.Result.response.zestimate.amount.Value, c.Result.response.url));

                foreach (SimpleProperty p in comp.Result.response.properties.comparables)
                {
                    Console.WriteLine(string.Format("{0}, {1} - {2}", p.address.latitude, p.address.longitude, p.zestimate.amount.Value));
                }
            }
            */

            string filePath = @"C:C:\Users\Parox\Desktop\bigdata\TXtaxData\2011.xls";
            
            if (File.Exists(filePath))
            {
                Console.WriteLine("File Found");
            }
            else
            {
                Console.WriteLine("Error. File not found");
            }

            // testing the class
            Excel excel = new Excel(filePath, 1);

            Console.WriteLine(excel.readCell(20, 0));

            Console.ReadLine();

            //starting to assemble a method to reading the important parts of the tax data
            List<ZipCodeData> taxData = new List<ZipCodeData>();
            int x = 14; 
            int y = 0;

            while(excel.readCell(x,y)!= 99999){
                ZipCodeData entry = new ZipCodeData();
                entry.zipCodeID = excel.readCell(x, y).ToString();
                
                y += 2; // moves over 2 columns
                entry.totalReturns = (int)excel.readCell(x, y);
                taxData.Add(entry);

                x += 5;
                entry.returnsAbove100k = (int)excel.readCell(x, y);
                taxData.Add(entry);

                x += 1;
                entry.returnsAbove200k = (int)excel.readCell(x, y);
                taxData.Add(entry);

                Console.WriteLine(entry.zipCodeID);
                Console.WriteLine(entry.totalReturns);
                Console.WriteLine(entry.returnsAbove100k);
                Console.WriteLine(entry.returnsAbove200k);

                // moves down 2 rows, to the next new zipcode
                // resets to the first column
                y = 0;
                x += 2;
                //Console.ReadLine(); // pause
            }
            Console.ReadLine();

            // NEW PROGRAM FOR READING THE CUSTOM LIST

            //string filePath = @"C:\Users\marti\source\repos\BigDataAnalyticsZillow\2011testdata.txt";
            //StreamReader fileReader = new StreamReader(filePath);


        }
    }
}