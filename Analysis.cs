using System;
using System.IO;
using System.Threading.Tasks;
using Zillow.Services.Schema;
using System.Collections.Generic;


namespace Zillow.Services
{
    class Analysis
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\marti\source\repos\BigDataAnalyticsZillow\2011testdata.txt";
            StreamReader fileReader = new StreamReader(filePath);
            Console.WriteLine(File.Exists(filePath));
            List<ZipCodeData> TX2011data = new List<ZipCodeData>();


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
                TX2011data.Add(entry);                
            }

            foreach(var entry in TX2011data)
            {
                Console.WriteLine(entry.toString());
            }
            Console.ReadLine();
        }
    }
}