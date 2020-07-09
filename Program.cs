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
            string stringTest = "https://www.aruodas.lt/sklypai-pardavimui/kaune/aleksote/puslapis/1/?FOfferType=1&FOrder=Price&detailed_search=1&fbclid=IwAR1EhPVF4OAQd_S_ju72eJcGYPbJJv8YpWzeNzo-adnT2y7-SlqgEtNttzY";
            string amountOfPages;
            var CsvFileName = @"Data.csv";
            var realEstates = new List<RealEstate>();
            List<string> urls = new List<string>();
            Console.WriteLine("Labas mantai :D Parašyk kiek puslapių išviso tavo pasirinkimas turi ir tada see the magic happen :D");
            Console.WriteLine("Tik numerį be tarpų parašyk.");
            amountOfPages = Console.ReadLine();
            for (int i = 1; i <= int.Parse(amountOfPages); i++)
            {
                string localUrl = stringTest.Replace("puslapis/1", "puslapis/" + i);
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
