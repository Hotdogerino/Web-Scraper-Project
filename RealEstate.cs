using System;
using System.Collections.Generic;
using System.Text;

namespace Web_Scraper_Project
{
    class RealEstate
    {
        public string Adress { get; set; }
        public decimal Price { get; set; }
        public decimal PricePerAcre { get; set; }
        //public double Plot { get; set; }
        //public string Intendance { get; set; }
        public int PriceDecrease { get; set; }

        public RealEstate(string adress, decimal price, decimal pricePerAcre, /*double plot, string intendance,*/ int priceDecrease)
        {
            Adress = adress;
            Price = price;
            PricePerAcre = pricePerAcre;
            //Plot = plot;
            //Intendance = intendance;
            PriceDecrease = priceDecrease;
        }

        public override string ToString()
        {
            return string.Format("{0};{1};{2}", Adress, Price, PricePerAcre);
        }
    }
}
