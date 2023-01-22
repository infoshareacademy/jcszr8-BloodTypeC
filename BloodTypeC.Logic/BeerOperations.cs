using BloodTypeC.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BloodTypeC.Logic
{
    public static class BeerOperations
    {
        public static List<Beer> SearchByName(List<Beer> listToSearch, string name)
        {
            return listToSearch.Where(beer => beer.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public static List<Beer> SearchByBrewery(List<Beer> listToSearch, string brewery)
        {
            return listToSearch.Where(beer => beer.Brewery.Equals(brewery, StringComparison.InvariantCultureIgnoreCase)).ToList();
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
        public static void DisplayBeer(List<Beer> listToDisplay, bool wait)
        {
            if (listToDisplay.Count > 0)
            {
                int formatLenght = String.Format("|{0,-20}|{1,-30}|{2,-30}|{3,-20}|{4,-20}|{5,-50}|{6,-20}|", "ID", "Name", "Brewery", "Style", "AlcoholByVolume", "Beer flavors", "Score").Length;
                for (int i = 0; i < formatLenght; i++)
                {
                    Console.Write("-");
                }
                Console.Write("\n");
                Console.WriteLine(String.Format("|{0,-20}|{1,-30}|{2,-30}|{3,-20}|{4,-20}|{5,-50}|{6,-20}|", "ID", "Name", "Brewery", "Style", "AlcoholByVolume", "Beer flavors", "Score"));
                for (int i = 0; i < formatLenght; i++)
                {
                    Console.Write("-");
                }
                Console.Write("\n");
                //Console.WriteLine($"{"ID".PadRight(10)} | {"Name".PadRight(20)} | {"Brewery".PadRight(20)} | {"Score".PadRight(10)} | {"AlcoholByVolume".PadRight(15)} | {"Flavors".PadRight(50)}");
                foreach (var item in listToDisplay)
                {
                    string beerFlavors = string.Join(",", item.Flavors);
                    Console.WriteLine(String.Format("|{0,-20}|{1,-30}|{2,-30}|{3,-20}|{4,-20}|{5,-50}|{6,-20}|", item.Id, item.Name, item.Brewery, item.Style, item.AlcoholByVolume, beerFlavors, item.Score));
                    //Console.WriteLine($"{item.Id.PadRight(10)} | {item.Name.PadRight(20)} | {item.Brewery.PadRight(20)} | {item.Score.ToString().PadRight(10)} | {item.AlcoholByVolume.ToString().PadRight(15)} | {beerFalvors.PadRight(50)}");                
                }
                if (wait)
                {
                    Console.WriteLine("Search successful. Press any key to return to main menu.");
                    Console.ReadKey();
                }
                return;
            }
            Console.WriteLine("There are no beers with specified name :( Please try again!");
            Console.ReadKey();
        }
        public static void NewBeer()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Beer beerToAdd = new Beer();

            // Name
            Console.WriteLine("\n> To add a beer, first we need the name of the beer.");
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Sorry. We cannot add a beer without a name.");
                Console.ReadKey(true);
                Console.CursorVisible = false;
                return;
            }
            beerToAdd.Name = Format.AsNameOrTitle(input, Format.CapitalsOptions.FirstWord, false);

            // Brewery
            Console.WriteLine("\n> Now tell us the name of the brewery.");
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Ok, the brewery is a mystery.");
                input = "Unknown";
            }
            beerToAdd.Brewery = Format.AsNameOrTitle(input, Format.CapitalsOptions.EachWord, false);

            //Style
            Console.Write("\n> What style is it?\n   (add new or choose from: ");
            string[] stylesFromDB = DB.AllBeers.Where(x => x.Flavors != null).Select(x => x.Style).Distinct().ToArray();
            Console.Write(string.Join(", ", stylesFromDB));
            Console.WriteLine(")");

            input = Console.ReadLine();
            string reformedInput = input;
            if (!string.IsNullOrEmpty(input))
            {
                reformedInput = Format.AsNameOrTitle(reformedInput, Format.CapitalsOptions.EachWord, true);
                beerToAdd.Style = reformedInput;
            }
            else
            {
                Console.WriteLine("Sure, the style can be unknown, why not.");
            }
            if (string.IsNullOrEmpty(reformedInput) && !string.IsNullOrEmpty(input))
            {
                Console.WriteLine("That style format is not acceptable.");
            }

            // Flavors
            Console.WriteLine("\n> What is the beer's taste like?\n   Use commas (,) and/or spaces when adding multiple flavors.");
            Console.Write("   Fellow tasters have reported other beers to be:\n  [");
            List<string> flavorsFromDB = DB.AllBeers.Where(x => x.Flavors != null).SelectMany(beer => beer.Flavors).Distinct().ToList();
            Console.Write(string.Join(", ", flavorsFromDB));
            Console.WriteLine("]");
            input = Console.ReadLine();
            var flavors = Format.AsTags(input);
            if (flavors.Count > 0)
            {
                beerToAdd.Flavors = flavors;
            }
            else if (!string.IsNullOrEmpty(input) && flavors.Count == 0)
            {
                Console.WriteLine("The flavors are badly formatted.");
            }
            else
            {
                Console.WriteLine("Fine, the flavors remain to be disovered.");
            }

            // Abv
            Console.WriteLine("\n> How much alcohol by volume does it have?");
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("That's key info we are missing...");
            }
            beerToAdd.AlcoholByVolume = Format.AsScoreOrABV(input, 94.99);
            Console.WriteLine($"We will set the abv to {beerToAdd.AlcoholByVolume}%.");

            // Score
            Console.WriteLine("\n> What is your score for this beer? 1-10");
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("No score, understood.");
            }
            beerToAdd.Score = Format.AsScoreOrABV(input, 10);
            Console.WriteLine($"We will set the score to {beerToAdd.Score} then.");

            Console.CursorVisible = false;
            beerToAdd.Add();
        }
        public static void EditBeer()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Enter the name of the beer that you would like to edit");
            string beerNameForSearch = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(beerNameForSearch))
            {
                var beersResult = BeerOperations.SearchByName(DB.AllBeers, beerNameForSearch);
                if (beersResult.Count == 0)
                {
                    Console.WriteLine("The entered name is invalid. Please enter a proper name of a beer.");
                    Console.ReadKey();
                    return;
                }
                BeerOperations.DisplayBeer(beersResult, false);
                Console.WriteLine("Would you like to edit the name? (Pressing enter will proceed without change.)");
                string newName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    beersResult[0].Name = Format.AsNameOrTitle(newName, Format.CapitalsOptions.FirstWord, false);
                }
                Console.WriteLine("Would you like to edit the brewery? (Pressing enter will proceed without change.)");
                string newBrewery = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newBrewery))
                {
                    beersResult[0].Brewery = Format.AsNameOrTitle(newBrewery, Format.CapitalsOptions.EachWord, false);
                }
                Console.WriteLine("Would you like to edit the style? (Pressing enter will proceed without change.)");
                string newStyle = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newStyle))
                {
                    beersResult[0].Style = Format.AsNameOrTitle(newStyle, Format.CapitalsOptions.EachWord, true);
                }
                Console.WriteLine("Would you like to edit the alcohol volume? (Pressing enter will proceed without change.)");
                string newABV = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newABV))
                {
                    beersResult[0].AlcoholByVolume = Format.AsScoreOrABV(newABV, 94.99);
                }
                Console.WriteLine("Editing flavors:");
                string newFlavors = string.Empty;
                foreach (var item in beersResult[0].Flavors)
                    /// to do
                {
                    Console.WriteLine($"Would you like to edit: {item}? (Pressing enter will proceed without change.)");
                    var flavorInput = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(flavorInput))
                    {
                        newFlavors += $"{item},";
                    }
                    else
                    {
                        flavorInput = flavorInput.Remove(flavorInput.IndexOf(" "));
                        flavorInput = flavorInput.Remove(flavorInput.IndexOf(","));
                        newFlavors += $"{flavorInput},";
                    }
                }
                Console.WriteLine("Add new flavors or press enter.");
                newFlavors += Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newFlavors))
                {
                    var newFlavorsFormated = Format.AsTags(newFlavors);
                    beersResult[0].Flavors = newFlavorsFormated;
                }
                Console.WriteLine("Would you like to edit the score? (Pressing enter will proceed without change.)");
                string newScore = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newScore))
                {
                    beersResult[0].Score = Format.AsScoreOrABV(newScore, 10);
                }
                Console.WriteLine("Press any key to return to main menu.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("The entered name is invalid. Please enter a proper name of a beer.");
            Console.ReadKey();
            Console.CursorVisible = false;
        }
    }
}
