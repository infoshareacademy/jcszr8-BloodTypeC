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
        public static List<Beer> SearchByStyle(List<Beer> listToSearch, string style)
        {
            return listToSearch.Where(beer => beer.Style.Contains(style, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public static List<Beer> SearchByFlavor(List<Beer> listToSearch, string searchflavor)
        {          
           return listToSearch.Where(beer => beer.Flavors.Contains(searchflavor,StringComparer.InvariantCultureIgnoreCase)).ToList();
        }

        public static List<Beer> SearchByAlcVol(List<Beer> listToSearch, double minAbv,double maxAbv)
        {

            return listToSearch.Where(beer => beer.AlcoholByVolume >= minAbv && beer.AlcoholByVolume <= maxAbv).ToList();
        }
        public static void DisplayBeer(List<Beer> listToDisplay) 
        {
            if (listToDisplay.Count > 0)
            {
                int formatLenght = String.Format("|{0,-20}|{1,-30}|{2,-30}|{3,-20}|{4,-20}|{5,-50}|{6,-20}|", "ID", "Name", "Brewery", "Style", "AlcoholByVolume", "Beer falvors" ,"Score").Length;
                for (int i = 0; i < formatLenght ; i++)
                {
                    Console.Write("-");
                }
                Console.Write("\n");
                Console.WriteLine(String.Format("|{0,-20}|{1,-30}|{2,-30}|{3,-20}|{4,-20}|{5,-50}|{6,-20}|", "ID", "Name", "Brewery", "Style", "AlcoholByVolume", "Beer falvors", "Score"));
                for (int i = 0; i < formatLenght; i++)
                {
                    Console.Write("-");
                }
                Console.Write("\n");
                //Console.WriteLine($"{"ID".PadRight(10)} | {"Name".PadRight(20)} | {"Brewery".PadRight(20)} | {"Score".PadRight(10)} | {"AlcoholByVolume".PadRight(15)} | {"Flavors".PadRight(50)}");
                foreach (var item in listToDisplay)
                {
                    string beerFalvors = string.Join(",",item.Flavors);
                    Console.WriteLine(String.Format("|{0,-20}|{1,-30}|{2,-30}|{3,-20}|{4,-20}|{5,-50}|{6,-20}|", item.Id, item.Name, item.Brewery, item.Style, item.AlcoholByVolume, beerFalvors, item.Score));
                    //Console.WriteLine($"{item.Id.PadRight(10)} | {item.Name.PadRight(20)} | {item.Brewery.PadRight(20)} | {item.Score.ToString().PadRight(10)} | {item.AlcoholByVolume.ToString().PadRight(15)} | {beerFalvors.PadRight(50)}");                
                }
                Console.ReadKey();
                return;
            }
            Console.WriteLine("There is no beers with specified name :( Please try again!");
            Console.ReadKey();
        }
        
    }
}
