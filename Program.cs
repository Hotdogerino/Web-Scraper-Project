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

namespace Web_Scraper_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            GetHtmlAsync();
            Console.ReadLine();
        }
        private static async void GetHtmlAsync()
        {
            var url = "https://www.aruodas.lt/sklypai-pardavimui/kaune/aleksote/?FOfferType=1&FOrder=Price&detailed_search=1&fbclid=IwAR1EhPVF4OAQd_S_ju72eJcGYPbJJv8YpWzeNzo-adnT2y7-SlqgEtNttzY";

            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var productsHtml = htmlDocument.DocumentNode.Descendants("table")
                .Where(x => x.GetAttributeValue("class", "")
                .Equals("list-search")).ToList();

            var productList = productsHtml[0].Descendants("tr")
                .Where(x => x.GetAttributeValue("class", "")
                .Contains("list-row")).ToList();

            // adress list

            var adressPriceList = new List<HtmlNode>();
            foreach (var ProductListItemAdress in productList)
            {
                adressPriceList.AddRange(ProductListItemAdress.Descendants("td")
                .Where(x => x.GetAttributeValue("class", "")
                .Equals("list-adress")).ToList());
            }

            // area overall list

            var areaOverall = new List<HtmlNode>();
            foreach (var ProductListItemArea in productList)
            {
                areaOverall.AddRange(ProductListItemArea.Descendants("td")
                .Where(x => x.GetAttributeValue("class", "")
                .Equals("list-AreaOverall")).ToList());
            }

            // intendances list

            var intendances = new List<HtmlNode>();
            foreach (var ProductListItemIntendences in productList)
            {
                intendances.AddRange(ProductListItemIntendences.Descendants("td")
                .Where(x => x.GetAttributeValue("class", "")
                .Equals("list-Intendances")).ToList());
            }

            //prints out adress


            foreach (var ProductListItemPrices in adressPriceList)
            {
                Console.OutputEncoding = Encoding.UTF8;

                Console.WriteLine(ProductListItemPrices.Descendants("h3").FirstOrDefault().InnerText.Trim());

                Console.WriteLine(ProductListItemPrices.Descendants("div")
                .Where(x => x.GetAttributeValue("class", "")
                .Equals("price")).FirstOrDefault().InnerText.Replace(" ", string.Empty));
            }

            Console.WriteLine();

        }

    }
}
