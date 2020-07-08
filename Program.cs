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
            var adressPriceList = new List<HtmlNode>();
            var areaOverall = new List<HtmlNode>();
            var intendances = new List<HtmlNode>();
            var adresses = new List<HtmlNode>();
            await InputOutput.GetHtmlAsync(adressPriceList, areaOverall, intendances, adresses);
            if (File.Exists("DataAdressPrice.txt"))
            {
                File.Delete("DataAdressPrice.txt");
            }
            InputOutput.PrintInfoToTxt("DataAdressPrice.txt", "DataArea.txt", "DataIntendances.txt", adressPriceList, areaOverall, intendances);
            InputOutput.ReadData("DataAdressPrice.txt", "DataArea.txt", "DataIntendances.txt", adressPriceList, areaOverall, intendances);
            Console.ReadLine();
        }
       

    }
}
