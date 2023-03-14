using BloodTypeC.ConsoleUI;
using BloodTypeC.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public static void DisplayBeer(List<Beer> listToDisplay, bool wait)
        {
            if (listToDisplay.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine();
                int formatLength = string.Format("|{0,-20}|{1,-30}|{2,-30}|{3,-20}|{4,-20}|{5,-50}|{6,-20}|", "ID", "Name", "Brewery", "Style", "AlcoholByVolume", "Beer flavors", "Score").Length;
                for (int i = 0; i < formatLength; i++)
                {
                    Console.Write("-");
                }
                Console.Write("\n");
                Console.WriteLine(string.Format("|{0,-20}|{1,-30}|{2,-30}|{3,-20}|{4,-20}|{5,-50}|{6,-20}|", "ID", "Name", "Brewery", "Style", "AlcoholByVolume", "Beer flavors", "Score"));
                for (int i = 0; i < formatLength; i++)
                {
                    Console.Write("-");
                }
                Console.Write("\n");
                //Console.WriteLine($"{"ID".PadRight(10)} | {"Name".PadRight(20)} | {"Brewery".PadRight(20)} | {"Score".PadRight(10)} | {"AlcoholByVolume".PadRight(15)} | {"Flavors".PadRight(50)}");
                foreach (var item in listToDisplay)
                {
                    string beerFlavors = "";
                    if (item.Flavors is not null)
                    {
                        beerFlavors = string.Join(",", item.Flavors);
                    }
                    Console.WriteLine(string.Format("|{0,-20}|{1,-30}|{2,-30}|{3,-20}|{4,-20}|{5,-50}|{6,-20}|", item.Id, item.Name, item.Brewery, item.Style, item.AlcoholByVolume, beerFlavors, item.Score));
                    //Console.WriteLine($"{item.Id.PadRight(10)} | {item.Name.PadRight(20)} | {item.Brewery.PadRight(20)} | {item.Score.ToString().PadRight(10)} | {item.AlcoholByVolume.ToString().PadRight(15)} | {beerFalvors.PadRight(50)}");                
                }
                Console.ResetColor();
                Console.WriteLine();
                if (wait)
                {
                    CWS.ColoredMsg("Search successful. Press any key to return to main menu.", "green");
                    Console.ReadKey();
                }
                return;
            }
            CWS.ColoredMsg("There are no beers matching the criteria :(", "red");
            Console.ReadKey();
        }
        public static void AddBeer(Beer beerToAdd)
        {
            beerToAdd.Id = (DB.AllBeers.Max(x => int.Parse(x.Id)) + 1).ToString();
            DateTime dateTimeNow = DateTime.Now;
            beerToAdd.Added = dateTimeNow;
            beerToAdd.Name = Format.AsNameOrTitle(beerToAdd.Name, Format.CapitalsOptions.EachWord, false);
            if (!string.IsNullOrWhiteSpace(beerToAdd.Brewery))
            {
                beerToAdd.Brewery = Format.AsNameOrTitle(beerToAdd.Brewery, Format.CapitalsOptions.EachWord, false);
            }
            if (!string.IsNullOrWhiteSpace(beerToAdd.Style))
            {
                beerToAdd.Style = Format.AsNameOrTitle(beerToAdd.Style, Format.CapitalsOptions.EachWord, true);
            }
            if (beerToAdd.Flavors.Any())
            {
                beerToAdd.Flavors = Format.AsTags(string.Join(",", beerToAdd.Flavors));
            }
            beerToAdd.Score = Format.AsScoreOrABV(beerToAdd.Score.ToString(), 10);
            beerToAdd.AlcoholByVolume = Format.AsScoreOrABV(beerToAdd.AlcoholByVolume.ToString(), 94.99);

            DB.AllBeers.Add(beerToAdd);
        }
        public static void EditBeer()
        {
            Console.Clear();
            Console.CursorVisible = true;
            CWS.ColoredMsg("EDITING A BEER\n==============\n", "yellow");
            CWS.ColoredMsg("Enter the full name of the beer that you would like to edit.\n", "yellow");
            string beerNameForSearch = CWS.ReadLine();
            bool edited = false;
            if (!string.IsNullOrWhiteSpace(beerNameForSearch))
            {
                var beersResult = BeerOperations.SearchByName(DB.AllBeers, beerNameForSearch).FirstOrDefault(x => string.Equals(beerNameForSearch, x.Name, StringComparison.InvariantCultureIgnoreCase));
                if (beersResult == null)
                {
                    CWS.ColoredMsg("We have no beers matching this name.", "red");
                    Console.ReadKey();
                    return;
                }
                var tempBeerList = new List<Beer> { beersResult };
                BeerOperations.DisplayBeer(tempBeerList, false);
                CWS.ColoredMsg("Would you like to edit the name?\n", "yellow");
                CWS.ColoredMsg(" (Pressing enter will proceed without any changes.)\n", "darkgray");
                string newName = CWS.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    beersResult.Name = Format.AsNameOrTitle(newName, Format.CapitalsOptions.FirstWord, false);
                    edited = true;
                }
                CWS.ColoredMsg("Would you like to edit the brewery?\n", "yellow");
                CWS.ColoredMsg(" (Pressing enter will proceed without any changes.)\n", "darkgray");
                string newBrewery = CWS.ReadLine();
                if (!string.IsNullOrWhiteSpace(newBrewery))
                {
                    beersResult.Brewery = Format.AsNameOrTitle(newBrewery, Format.CapitalsOptions.EachWord, false);
                    edited = true;
                }
                CWS.ColoredMsg("Would you like to edit the style?\n", "yellow");
                CWS.ColoredMsg(" (Pressing enter will proceed without any changes.)\n", "darkgray");
                string newStyle = CWS.ReadLine();
                if (!string.IsNullOrWhiteSpace(newStyle))
                {
                    beersResult.Style = Format.AsNameOrTitle(newStyle, Format.CapitalsOptions.EachWord, true);
                    edited = true;
                }
                CWS.ColoredMsg("Would you like to edit the alcohol volume?\n", "yellow");
                CWS.ColoredMsg(" (Pressing enter will proceed without any changes.)\n", "darkgray");
                string newABV = CWS.ReadLine();
                if (!string.IsNullOrWhiteSpace(newABV))
                {
                    beersResult.AlcoholByVolume = Format.AsScoreOrABV(newABV, 94.99);
                    edited = true;
                }
                CWS.ColoredMsg("Editing flavors.\n", "darkgray");
                CWS.ColoredMsg("  Press enter to proceed without any changes. Use one word only.\n  Anything past a non-letter character will be ignored.\n", "darkgray");
                string newFlavors = string.Empty;
                foreach (var oldFlavor in beersResult.Flavors)
                {
                    CWS.ColoredMsg($"> Would you like to replace: ", "yellow");
                    CWS.ColoredMsg($"{oldFlavor}", "darkcyan");
                    CWS.ColoredMsg($"?\n", "yellow");
                    var flavorInput = CWS.ReadLine().Trim();
                    if (string.IsNullOrWhiteSpace(flavorInput))
                    {
                        newFlavors += $"{oldFlavor},";
                    }
                    else
                    {
                        while (flavorInput.Contains(',') && flavorInput[0] == ',')
                        {
                            flavorInput = flavorInput.Substring(1);
                        }
                        if (flavorInput.Contains(" "))
                        {
                            flavorInput = flavorInput.Remove(flavorInput.IndexOf(" "));
                        }
                        if (flavorInput.IndexOf(",") > 0)
                        {
                            flavorInput = flavorInput.Remove(flavorInput.IndexOf(","));
                        }
                        newFlavors += $"{flavorInput},";
                        edited = true;
                    }
                }
                CWS.ColoredMsg("Add new flavors or press enter.\n", "yellow");
                CWS.ColoredMsg("Separate multiple flavors with a space (or any other non-letter character, whatever).\n", "darkgray");
                string input = CWS.ReadLine().Trim();
                newFlavors += input;
                if (!string.IsNullOrWhiteSpace(newFlavors))
                {
                    var newFlavorsFormated = Format.AsTags(newFlavors);
                    beersResult.Flavors = newFlavorsFormated;
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        edited = true;
                    }
                }
                CWS.ColoredMsg("Would you like to edit the score?\n", "yellow");
                CWS.ColoredMsg(" (Pressing enter will proceed without any changes.)\n", "darkgray");
                string newScore = CWS.ReadLine();
                if (!string.IsNullOrWhiteSpace(newScore))
                {
                    beersResult.Score = Format.AsScoreOrABV(newScore, 10);
                    edited = true;
                }
                if (edited)
                {
                    beersResult.LastModified = DateTime.Now;
                    CWS.ColoredMsg("Changes have been made!\n", "green");
                }
                else
                {
                    CWS.ColoredMsg("No changes have been made.\n", "red");
                }
                CWS.ColoredMsg("Press any key to return to main menu.", "blue");
                Console.ReadKey();
                return;
            }
            CWS.ColoredMsg("The entered name is invalid. Please enter a proper name of a beer.", "red");
            Console.ReadKey();
            Console.CursorVisible = false;
        }
        public static void AddToFavs(int id)
        {
            var beer = DB.AllBeers.FirstOrDefault(x => x.Id == id.ToString());
            DB.FavoriteBeers?.Add(beer);
        }
        public static void RemoveFromFavs(int id)
        {
            var beer = DB.FavoriteBeers.FirstOrDefault(x => x.Id == id.ToString());
            DB.FavoriteBeers?.Remove(beer);
        }

        public static List<string> GetAllFlavors()
        {
            return DB.AllBeers.Where(x => x.Flavors != null).SelectMany(beer => beer.Flavors).Distinct().ToList();
        }
        public static void GetAllBeers()
        {
        }
    }
    }

