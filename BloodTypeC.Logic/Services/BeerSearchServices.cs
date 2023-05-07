using BloodTypeC.DAL.Models;
using BloodTypeC.Logic.Services.IServices;

namespace BloodTypeC.Logic.Services
{
    public class BeerSearchServices : IBeerSearchServices
    {
        public List<Beer> SearchByName(List<Beer> listToSearch, string name)
        {
            return listToSearch.Where(beer => beer.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public List<Beer> SearchByBrewery(List<Beer> listToSearch, string brewery)
        {
            return listToSearch.Where(beer => beer.Brewery.Contains(brewery, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
        public List<Beer> SearchByStyle(List<Beer> listToSearch, string style)
        {
            return listToSearch.Where(beer => beer.Style.Contains(style, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public List<Beer> SearchByFlavor(List<Beer> listToSearch, string searchflavor)
        {
            return listToSearch.Where(beer => beer.Flavors.Contains(searchflavor, StringComparer.InvariantCultureIgnoreCase)).ToList();
        }

        public List<Beer> SearchByAlcVol(List<Beer> listToSearch, double minAbv, double maxAbv)
        {
            return listToSearch.Where(beer => beer.AlcoholByVolume >= minAbv && beer.AlcoholByVolume <= maxAbv).ToList();
        }

        public List<string> GetAllFlavors(List<Beer> beers)
        {
            return beers.Where(x => x.Flavors != null).SelectMany(beer => beer.Flavors).Distinct().ToList();
        }
    }
}
