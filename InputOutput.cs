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
        static readonly HttpClient httpClient = new HttpClient();
        public static async Task GetHtmlAsync(List<RealEstate> realEstates, string urlName, int currentPage, int lastPage)
        {
            var url = urlName;

            //var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var productsHtml = htmlDocument.DocumentNode.Descendants("table")
                .Where(x => x.GetAttributeValue("class", "")
                .Equals("list-search")).ToList();
            var productList = productsHtml[0].Descendants("tr")
                .Where(x => x.GetAttributeValue("class", "").Trim()
                .Contains("list-row")).ToList();
            foreach (var item in productList.ToList())
            {
                if (item.InnerText.Trim().Length == 0 || item.InnerText.Trim().Contains("Kiti šio arba itin panašaus objekto skelbimai"))
                {
                    productList.Remove(item);
                }
            }
            //for (int i = 0; i < productList.Count; i++)
            //{
            //    if (productList[i].InnerText.Trim().Length == 0)
            //    {
            //        productList.Remove(productList[i]);
            //    }
            //}

            foreach (var item in productList)
            {
                var price = item.Descendants("span")
                .Where(x => x.GetAttributeValue("class", "")
                .Equals("list-item-price"));

                var adress = item.Descendants("h3");

                var areaOveralls = item.Descendants("td")
                .Where(x => x.GetAttributeValue("class", "")
                .Equals("list-AreaOverall"));

                var intendance = item.Descendants("td")
                .Where(x => x.GetAttributeValue("class", "")
                .Equals("list-Intendances"));
                if (price == null || adress == null || areaOveralls == null || intendance == null)
                {
                    throw new NullReferenceException("Vienas objektų yra null (t.y vieno objekto reikšmė yra niekas)");
                }
                
                RealEstate realEstate = new RealEstate(adress.First().InnerText.Trim(), price.First().InnerText.Replace("€", "").Replace(" ", ""), areaOveralls.First().InnerText.Trim(),
                    intendance.First().InnerText.Trim());
                realEstates.Add(realEstate);
                //realEstatesHashSet.Add(realEstate);

               
            }

            realEstates.Distinct().ToList();
            //realEstatesHashSet.Distinct().ToList();

            Console.WriteLine("Nuskaitymas pavyko! (" + currentPage + "/" + lastPage + ")");

        }
        //public static void CheckIfItHasDupes(List<RealEstate> realEstates)
        //{
        //    var duplicatedKey = realEstates.GroupBy(x =>new { x.Adress, x.Price, x.Intendance, x.Plot }).Where(x => x.Count() > 1).Select(x => x.Key);

        //    var duplicated = realEstates.FindAll(p => duplicatedKey.Contains(p.Adress));
        //}
        public static void PrintListToCSV(string fileName, List<RealEstate> realEstates)
        {
            using (StreamWriter wr = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                wr.WriteLine("{0};{1};{2};{3}",
                    "Adresas", "Kaina", "Koks Plotas", "Paskirtis");
                var listDistinct = realEstates.Distinct().ToList();
                foreach (var item in listDistinct)
                {
                    wr.WriteLine("{0};{1};{2};{3}", item.Adress, item.Price, item.Plot, item.Intendance);
                }
                               
            }
            Console.WriteLine("CSV failas sukurtas!");
        }





        //public static void PrintInfoToTxt(string fileName, string filename2, string filename3, List<HtmlNode> adressPriceList, List<HtmlNode> areaOverall, List<HtmlNode> intendances)
        //{
        //    using (StreamWriter writer = File.AppendText(fileName))
        //    {
        //        foreach (var ProductListItemPrices in adressPriceList)
        //        {
        //            writer.WriteLine(ProductListItemPrices.Descendants("h3").FirstOrDefault().InnerText.Trim());
        //            writer.WriteLine(ProductListItemPrices.Descendants("div")
        //        .Where(x => x.GetAttributeValue("class", "")
        //        .Equals("price")).FirstOrDefault().InnerText.Trim().Replace(" ", ""));
        //        }
        //    }
        //    using (StreamWriter writer2 = File.AppendText(filename2))
        //    {
        //        foreach (var ProductListItemArea in areaOverall)
        //        {

        //            writer2.WriteLine(ProductListItemArea.InnerText.Trim());
        //        }
        //    }
        //    using (StreamWriter writer3 = File.AppendText(filename3))
        //    {
        //        foreach (var ProductListItemIntendances in intendances)
        //        {
        //            writer3.WriteLine(ProductListItemIntendances.InnerText.Trim());
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



    // price list

    // foreach (var ProductListItemAdress in productList)
    // {
    //     //adressPriceList.Add
    //     var xd = (ProductListItemAdress.Descendants("span")
    //.Where(x => x.GetAttributeValue("class", "")
    //.Contains("list-item-price")));
    // }

    // // adress

    // foreach (var ProductListItemAdress in productList)
    // {
    //     adresses.AddRange(ProductListItemAdress.Descendants("h3").ToList());
    // }

    // // area overall list

    // foreach (var ProductListItemArea in productList)
    // {
    //     areaOverall.AddRange(ProductListItemArea.Descendants("td")
    //     .Where(x => x.GetAttributeValue("class", "")
    //     .Equals("list-AreaOverall")).ToList());
    // }

    // // intendances list

    // foreach (var ProductListItemIntendences in productList)
    // {
    //     intendances.AddRange(ProductListItemIntendences.Descendants("td")
    //     .Where(x => x.GetAttributeValue("class", "")
    //     .Equals("list-Intendances")).ToList());
    // }
}

