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
        public static List<Beer> SearchByName(List<Beer> listToSearch,string name) 
        {
            
            return listToSearch.Where(beer => beer.Name == name).ToList();
            
        }

        public static List<Beer> SearchByBrewery(List<Beer> listToSearch, string brewery)
        {
            
            return listToSearch.Where(beer => beer.Brewery == brewery).ToList();
            
        }

        public static List<Beer> SearchByFlavor(List<Beer> listToSearch, string searchflavor)
        {
            foreach (Beer beer in listToSearch) 
            {
                for (int i = 0; i < beer.Flavors.Count; i++)
                {
                    beer.Flavors[i] = beer.Flavors[i].ToLower();
                }
            }

           return listToSearch.Where(beer => beer.Flavors.Contains(searchflavor.ToLower())).ToList();
        }

        public static List<Beer> SearchByAlkVol(List<Beer> listToSearch, double abv)
        {
            
            return listToSearch.Where(beer => beer.AlcoholByVolume >= abv).ToList();
        }

    }
}
