using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Web_Scraper_Project
{
    class InputOutput
    {
        public static async void GetHtmlAsync(List<HtmlNode> adressPriceList, List<HtmlNode> areaOverall, List<HtmlNode> intendances)
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

            foreach (var ProductListItemAdress in productList)
            {
                adressPriceList.AddRange(ProductListItemAdress.Descendants("td")
                .Where(x => x.GetAttributeValue("class", "")
                .Equals("list-adress")).ToList());
            }

            // area overall list

            foreach (var ProductListItemArea in productList)
            {
                areaOverall.AddRange(ProductListItemArea.Descendants("td")
                .Where(x => x.GetAttributeValue("class", "")
                .Equals("list-AreaOverall")).ToList());
            }

            // intendances list

            foreach (var ProductListItemIntendences in productList)
            {
                intendances.AddRange(ProductListItemIntendences.Descendants("td")
                .Where(x => x.GetAttributeValue("class", "")
                .Equals("list-Intendances")).ToList());
            }


            foreach (var ProductListItemPrices in adressPriceList)
            {
                Console.OutputEncoding = Encoding.UTF8;

                Console.WriteLine(ProductListItemPrices.Descendants("h3").FirstOrDefault().InnerText.Trim());

                Console.WriteLine(ProductListItemPrices.Descendants("div")
                .Where(x => x.GetAttributeValue("class", "")
                .Equals("price")).FirstOrDefault().InnerText);
            }
            foreach (var ProductListItemArea in areaOverall)
            {
                Console.WriteLine(ProductListItemArea.InnerText);
            }
            foreach (var ProductListItemIntendances in intendances)
            {
                Console.WriteLine(ProductListItemIntendances.InnerText);
            }

            Console.WriteLine();

        }
        //public static void PrintListsToCSV(string fileName, List<HtmlNode> adressPriceList, List<HtmlNode> areaOverall, List<HtmlNode> intendances)
        //{
        //    using (StreamWriter wr = new StreamWriter(fileName, false, Encoding.UTF8))
        //    {
        //        wr.WriteLine("{0};{1};{2};{3};{4};{5};{6};{7}",
        //            "Vardas", "Pavardė", "Metai", "Ūgis", "Pozicija", "Klubas", "Pakviestas?", "Kapitonas?");
        //        for (int i = 0; i < adressPriceList.Count; i++)
        //        {
        //            wr.WriteLine("{0};{1};{2};{3};{4};{5};{6};{7}",
        //                adressPriceList[i].Name, players[i].Surname, players[i].BirthDate.ToString("d"), players[i].Height, players[i].Position, players[i].Club, players[i].Invited, players[i].IsCaptain);
        //        }
        //    }
        //}
    }
}
