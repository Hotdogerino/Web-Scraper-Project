using System;
using System.Collections.Generic;
using System.Text;

namespace Web_Scraper_Project
{
    class RealEstate
    {
        public string Adress { get; set; }
        public string Price { get; set; }
        public string Plot { get; set; }
        public string Intendance { get; set; }

        public RealEstate(string adress, string price, string plot, string intendance)
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

        public override bool Equals(object obj)
        {
            return obj is RealEstate estate &&
                   Adress == estate.Adress &&
                   Price == estate.Price &&
                   Plot == estate.Plot &&
                   Intendance == estate.Intendance;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Adress, Price, Plot, Intendance);
        }
    }
}
