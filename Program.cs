using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.Unicode;
using System.IO;

namespace Web_Scraper_Project
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
            string stringTest;
            string amountOfPages;
            var CsvFileName = @"Data.csv";
            var realEstates = new List<RealEstate>();
            List<string> urls = new List<string>();
            Console.WriteLine("Parašyk kiek puslapių išviso tavo pasirinkimas turi");
            Console.WriteLine("įrašyk linką, bet kai rašysi linką įmesk jį su puslapiu antru (url turi tureti /puslapis/2/)");
            stringTest = Console.ReadLine();
            Console.WriteLine("Įrašyk numerį kiek yra išviso page'ų. Be tarpų parašyk.");
            amountOfPages = Console.ReadLine();
            for (int i = 1; i <= int.Parse(amountOfPages); i++)
            {
                string localUrl = stringTest.Replace("puslapis/2", "puslapis/" + i);
                urls.Add(localUrl);
            }
            for (int i = 0; i < urls.Count; i++)
            {
                await InputOutput.GetHtmlAsync(realEstates, urls[i], i+1, int.Parse(amountOfPages));
            }
            if (File.Exists("Data.csv"))
            {
                File.Delete("Data.csv");
            }
            InputOutput.PrintListToCSV(CsvFileName, realEstates);
            Console.ReadLine();
        }
       

    }
}
