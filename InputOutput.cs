using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Web_Scraper_Project
{
    class InputOutput
    {
        public static async Task GetHtmlAsync(List<HtmlNode> adressPriceList, List<HtmlNode> areaOverall, List<HtmlNode> intendances, List<HtmlNode> adresses)
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

            // price list

            foreach (var ProductListItemAdress in productList)
            {
                //adressPriceList.Add
                var xd = (ProductListItemAdress.Descendants("span")
           .Where(x => x.GetAttributeValue("class", "")
           .Contains("list-item-price")));
            }

            // adress

            foreach (var ProductListItemAdress in productList)
            {
                adresses.AddRange(ProductListItemAdress.Descendants("h3").ToList());
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


            foreach (var item in productList)
            {
                var price = (item.Descendants("span")
                .Where(x => x.GetAttributeValue("class", "")
                .Contains("list-item-price")));

                var adress = item.Descendants("h3");

                var areaOveralls = item.Descendants("td")
                .Where(x => x.GetAttributeValue("class", "")
                .Equals("list-AreaOverall"));

                var intendance = item.Descendants("td")
                .Where(x => x.GetAttributeValue("class", "")
                .Equals("list-Intendances"));
                //RealEstate realEstate = new RealEstate(adress.First().InnerText.Trim(), price.First().InnerText
            }





            Console.WriteLine(intendances.Count);
            Console.WriteLine();

        }
        public static void RemoveTrash(List<HtmlNode> adressPriceList)
        {
            adressPriceList.Where(x => x.InnerText.Contains(""));
        }
        public static void PrintInfoToTxt(string fileName, string filename2, string filename3, List<HtmlNode> adressPriceList, List<HtmlNode> areaOverall, List<HtmlNode> intendances)
        {
            using (StreamWriter writer = File.AppendText(fileName))
            {
                foreach (var ProductListItemPrices in adressPriceList)
                {
                    writer.WriteLine(ProductListItemPrices.Descendants("h3").FirstOrDefault().InnerText.Trim());
                    writer.WriteLine(ProductListItemPrices.Descendants("div")
                .Where(x => x.GetAttributeValue("class", "")
                .Equals("price")).FirstOrDefault().InnerText.Trim().Replace(" ", ""));
                }
            }
            using (StreamWriter writer2 = File.AppendText(filename2))
            {
                foreach (var ProductListItemArea in areaOverall)
                {

                    writer2.WriteLine(ProductListItemArea.InnerText.Trim());
                }
            }
            using (StreamWriter writer3 = File.AppendText(filename3))
            {
                foreach (var ProductListItemIntendances in intendances)
                {
                    writer3.WriteLine(ProductListItemIntendances.InnerText.Trim());
                }
            }
        }
        public static void ReadData(string filename, string filename2, string filename3, List<HtmlNode> adressPriceList, List<HtmlNode> areaOverall, List<HtmlNode> intendances)
        {

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
    //using (var file = new StreamReader(fileName, Encoding.UTF8))
    //{
    //    string line = "";
    //    while ((line = file.ReadLine()) != null && !line.Contains("Kainasumažėjusi"))
    //    {
    //        string text = file.ReadToEnd();
    //        string[] lines = text.Split(Environment.NewLine);

    //    }
    //}
    //string contents = File.ReadAllText(filename);
    //contents = contents.Replace("\r", "\n"); // You don't have to do this if you know your line endings are only \n
    //        List<RealEstate> entries = contents
    //            .Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
    //            .Select(x => {
    //                var y = x.Split("\n");
    //                return new RealEstate(
    //                    pricePerAcre: Convert.ToInt32(y[0].Replace("€/a", "")),
    //                    adress: y[1],
    //                    priceDecrease: Convert.ToInt32(y[2].Replace("Kainasumažėjusi", "").Replace("%", "")),
    //                    price: Convert.ToInt32(y[3].Replace("€", "")));
    //            })
    //            .ToList();
}

