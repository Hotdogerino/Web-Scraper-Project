using System;
using System.Collections.Generic;
using System.Text;

namespace Web_Scraper_Project
{
    class RealEstate
    {
        public string Adress { get; set; }
        public decimal Price { get; set; }
        public double Plot { get; set; }
        public string Intendance { get; set; }

        public RealEstate(string adress, decimal price, decimal pricePerAcre, double plot, string intendance)
        {
            Adress = adress;
            Price = price;
            Plot = plot;
            Intendance = intendance;

        }

        public override string ToString()
        {
            return string.Format("{0};{1};{2};{3}", Adress, Price, Plot, Intendance);
        }
    }
}
