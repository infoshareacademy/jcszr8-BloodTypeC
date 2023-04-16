using BloodTypeC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypeC.Logic.Services
{
    public class BeerSearchServices
    {
        public static List<Beer> SearchByName(List<Beer> listToSearch, string name)
        {
            return listToSearch.Where(beer => beer.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public static List<Beer> SearchByBrewery(List<Beer> listToSearch, string brewery)
        {
            return listToSearch.Where(beer => beer.Brewery.Contains(brewery, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
        public static List<Beer> SearchByStyle(List<Beer> listToSearch, string style)
        {
            return listToSearch.Where(beer => beer.Style.Contains(style, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public static List<Beer> SearchByFlavor(List<Beer> listToSearch, string searchflavor)
        {
            return listToSearch.Where(beer => beer.Flavors.Contains(searchflavor, StringComparer.InvariantCultureIgnoreCase)).ToList();
        }

        public static List<Beer> SearchByAlcVol(List<Beer> listToSearch, double minAbv, double maxAbv)
        {
            return listToSearch.Where(beer => beer.AlcoholByVolume >= minAbv && beer.AlcoholByVolume <= maxAbv).ToList();
        }

        public static List<string> GetAllFlavors(List<Beer> beers)
        {
            return beers.Where(x => x.Flavors != null).SelectMany(beer => beer.Flavors).Distinct().ToList();
        }
    }
}
