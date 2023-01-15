using BloodTypeC.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BloodTypeC.Logic
{
    public static class BeerSearch
    {
        public static List<Beer> SearchByName(List<Beer> listToSearch, string name) 
        {
            return listToSearch.Where(beer => beer.Name.Equals(name,StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public static List<Beer> SearchByBrewery(List<Beer> listToSearch, string brewery)
        {
            return listToSearch.Where(beer => beer.Brewery.Equals(brewery,StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public static List<Beer> SearchByFlavor(List<Beer> listToSearch, string searchflavor)
        {          
           return listToSearch.Where(beer => beer.Flavors.Contains(searchflavor,StringComparer.InvariantCultureIgnoreCase)).ToList();
        }

        public static List<Beer> SearchByAlkVol(List<Beer> listToSearch, double minAbv,double maxAbv)
        {

            return listToSearch.Where(beer => beer.AlcoholByVolume >= minAbv && beer.AlcoholByVolume <= maxAbv).ToList();
        }
        public static void DisplayBeer(List<Beer> listToDisplay) 
        {
            if (listToDisplay.Count > 0)
            {
                Console.WriteLine($"{"ID".PadRight(10)} | {"Name".PadRight(20)} | {"Brewery".PadRight(20)} | {"Score".PadRight(10)} | {"AlcoholByVolume".PadRight(5)}");
                foreach (var item in listToDisplay)
                {
                    Console.WriteLine($"{item.Id.PadRight(10)} | {item.Name.PadRight(20)} | {item.Brewery.PadRight(20)} | {item.Score.ToString().PadRight(10)} | {item.AlcoholByVolume.ToString().PadRight(5)}");
                }
            }
                Console.WriteLine("Brak piwa o podanej nazwie");
            
        }

    }
}
