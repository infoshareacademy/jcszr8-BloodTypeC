using BloodTypeC.DAL;
using BloodTypeC.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Console;

namespace BloodTypeC.ConsoleUI
{
    class Navigation
    {
        string logo = @"
 ______   _______  _______  _______         _______         _______  _______  ______  _________ _______ 
(  ___ \ (  ____ \(  ____ \(  ____ )       (  ___  )       (  ____ )(  ____ \(  __  \ \__   __/(  ___  )
| (   ) )| (    \/| (    \/| (    )|       | (   ) |       | (    )|| (    \/| (  \  )   ) (   | (   ) |
| (__/ / | (__    | (__    | (____)| _____ | |   | | _____ | (____)|| (__    | |   ) |   | |   | (___) |
|  __ (  |  __)   |  __)   |     __)(_____)| |   | |(_____)|  _____)|  __)   | |   | |   | |   |  ___  |
| (  \ \ | (      | (      | (\ (          | |   | |       | (      | (      | |   ) |   | |   | (   ) |
| )___) )| (____/\| (____/\| ) \ \__       | (___) |       | )      | (____/\| (__/  )___) (___| )   ( |
|/ \___/ (_______/(_______/|/   \__/       (_______)       |/       (_______/(______/ \_______/|/     \|
";
        public void Start()
        {
            Title = "Beer-o-pedia";
            RunStartMenu();
        }
        private void RunStartMenu()
        {
            string prompt = $"{logo}Welcome to Beer-o-pedia! Are you over 18?";
            string[] options = { "Yes", "No" };
            Menu startMenu = new Menu(prompt, options);
            int selectedIndex = startMenu.Run();
            Console.CursorVisible = false;


            if (selectedIndex == 1)
            {
                Clear();
                ForegroundColor = ConsoleColor.Red;
                WriteLine($"{logo}Oh you are so naughty! Run away or get spanked!");
                WriteLine("Press any key to exit!");
                ResetColor();
                ReadKey(true);
                Environment.Exit(0);
            }

            Load.LoadFromFile();

            while (true)
            {
                Console.CursorVisible = false;
                prompt = $"{logo}Beer-o-pedia main menu!";
                string[] options2 = {
                    "1. Search by beer name.",
                    "2. Search by brewery.",
                    "3. Search by style.",
                    "4. Search by alcohol volume.",
                    "5. Search by flavor.",
                    "6. Add a new beer.",
                    "7. Edit a beer.",
                    "8. Exit application." };
                Menu mainMenu = new Menu(prompt, options2);
                selectedIndex = mainMenu.Run();
                var beersResult = new List<Beer>();

                switch (selectedIndex)
                {
                    case 0: // Search by name
                        Console.Clear();
                        Console.CursorVisible = true;
                        Console.WriteLine("Enter the name of the beer that you are looking for:");
                        string beerNameForSearch = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(beerNameForSearch))
                        {
                            beersResult = BeerSearch.SearchByName(DB.AllBeers, beerNameForSearch);
                            BeerSearch.DisplayBeer(beersResult, true);
                            break;
                        }
                        Console.WriteLine("The entered name is invalid. Please enter a proper name of a beer.");
                        Console.ReadKey();
                        Console.CursorVisible = false;
                        break;
                    case 1: // Search by brewery
                        Console.Clear();
                        Console.CursorVisible = true;
                        Console.WriteLine("Enter the brewery name of the beer that you are looking for:");
                        string breweryNameForSearch = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(breweryNameForSearch))
                        {
                            beersResult = BeerSearch.SearchByBrewery(DB.AllBeers, breweryNameForSearch);
                            BeerSearch.DisplayBeer(beersResult,true);
                            break;
                        }
                        Console.WriteLine("The entered brewery name is invalid. Please enter a proper name of a beer.");
                        Console.ReadKey();
                        Console.CursorVisible = false;
                        break;
                    case 2: // Search by style
                        Console.Clear();
                        Console.CursorVisible = true;
                        Console.WriteLine("Enter the style of the beer that you are looking for:");
                        string beerStyleForSearch = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(beerStyleForSearch))
                        {
                            beersResult = BeerSearch.SearchByStyle(DB.AllBeers, beerStyleForSearch);
                            BeerSearch.DisplayBeer(beersResult, true);
                            break;
                        }
                        Console.WriteLine("Entered style is null or whitespace. Please enter proper style of beer.");
                        Console.ReadKey();
                        Console.CursorVisible = false;
                        break;

                    case 3: // Search by ABV
                        Console.Clear();
                        Console.CursorVisible = true;
                        double minAbv, maxAbv;
                        Console.WriteLine("Enter min ABV");
                        if (!double.TryParse(Console.ReadLine(), out minAbv))
                        {
                            Console.WriteLine("Error. The entered data was not a number.\nReturning to menu.");
                            Console.ReadKey();
                            break;
                        }
                        Console.WriteLine("Enter max ABV");
                        if (!double.TryParse(Console.ReadLine(), out maxAbv))
                        {
                            Console.WriteLine("Error. The entered data was not a number.\nReturning to menu.");
                            Console.ReadKey();
                            break;
                        }
                        beersResult = BeerSearch.SearchByAlcVol(DB.AllBeers, minAbv, maxAbv);
                        Console.Clear();
                        BeerSearch.DisplayBeer(beersResult, true);
                        Console.CursorVisible = false;
                        break;
                    case 4: // Search by flavors
                        Console.Clear();
                        Console.CursorVisible = true;
                        Console.WriteLine("Flavors in pedia:");
                        var flavors = DB.AllBeers.SelectMany(beer => beer.Flavors).Distinct().ToList();
                        Console.WriteLine(string.Join(", ", flavors));
                        string searchFlavor = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(searchFlavor))
                        {
                            beersResult = BeerSearch.SearchByFlavor(DB.AllBeers, searchFlavor);
                            BeerSearch.DisplayBeer(beersResult, true);
                            Console.ReadKey();
                        }
                        Console.WriteLine("Entered flavor is null or whitespace. Please enter proper flavor of beer.");
                        Console.ReadKey();
                        Console.CursorVisible = false;
                        break;
                    case 5:
                        NewBeer();
                        break;
                    case 6:
                        EditBeer();
                        break;
                    case 7:
                        Clear();
                        WriteLine($"{logo}Thank you for coming! Have a nice day!");
                        WriteLine("Press any key to close the application!");
                        ReadKey(true);
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }
        private void NewBeer()
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
            beerToAdd.Name = Format.AsNameOrTitle(input, Format.CapitalsOptions.FirstWord);

            // Brewery
            Console.WriteLine("\n> Now tell us the name of the brewery.");
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Ok, the brewery is a mystery.");
                input = "Unknown";
            }
            beerToAdd.Brewery = Format.AsNameOrTitle(input, Format.CapitalsOptions.EachWord);

            //Style
            Console.Write("\n> What style is it?\n   (add new or choose from:");
            string[] stylesFromDB = DB.AllBeers.Select(x => x.Style).Distinct().ToArray();
            Console.Write(string.Join(", ", stylesFromDB));
            Console.WriteLine(")");

            input = Console.ReadLine();
            bool wasReformed = false;
            if (!string.IsNullOrEmpty(input))
            {
                input = Regex.Replace(input.ToLower(), "[^a-z ąćęłńóśżź-]", " ");
                input = Regex.Replace(input, @"\s+", " ");
                input = Regex.Replace(input, @"-+", "-");
                input = Format.AsNameOrTitle(input, Format.CapitalsOptions.EachWord);
                wasReformed = true;
            }
            else
            {
                Console.WriteLine("Sure, the style can be unknown, why not.");
            }
            if (wasReformed == true && string.IsNullOrEmpty(input))
            {
                Console.WriteLine("That style format is not acceptable.");
            }
            else
            { 
                beerToAdd.Style = input;
            }

            // Flavors
            Console.WriteLine("\n> What is the beer's taste like?\n   Use commas (,) and/or spaces when adding multiple flavors.");
            Console.Write("   Fellow tasters have reported other beers to be:\n  [");
            List<string> flavorsFromDB = DB.AllBeers.SelectMany(beer => beer.Flavors).Distinct().ToList();
            Console.Write(String.Join(", ", flavorsFromDB));
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
            Console.WriteLine($"The score is {beerToAdd.Score} then.");

            Console.CursorVisible = false;
            beerToAdd.Add();
        }
        private void EditBeer()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Enter the name of the beer that you would like to edit");
            string beerNameForSearch = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(beerNameForSearch))
            {
                var beersResult = BeerSearch.SearchByName(DB.AllBeers, beerNameForSearch);
                BeerSearch.DisplayBeer(beersResult, false);
                Console.WriteLine("Would you like to edit the name? (Confirming blank space will proceed without change.)");
                string newName = Console.ReadLine();
                beersResult[0].Name = newName;
                if (string.IsNullOrEmpty(newName))
                {
                    Console.WriteLine("Name not edited");
                }
                beersResult[0].Name = Format.AsNameOrTitle(newName, Format.CapitalsOptions.FirstWord);
                Console.WriteLine("Would you like to edit the brewery? (Confirming blank space will proceed without change.)");
                string newBrewery = Console.ReadLine();
                beersResult[0].Brewery = newBrewery;
                beersResult[0].Brewery = Format.AsNameOrTitle(newBrewery, Format.CapitalsOptions.EachWord);
                Console.WriteLine("Would you like to edit the style? (Confirming blank space will proceed without change.)");
                string newStyle = Console.ReadLine();
                beersResult[0].Style = newStyle;
                beersResult[0].Style = Regex.Replace(newStyle.ToLower(), "[^a-z ąćęłńóśżź-]", " ");
                beersResult[0].Style = Regex.Replace(newStyle, @"\s+", " ");
                beersResult[0].Style = Regex.Replace(newStyle, @"-+", "-");
                beersResult[0].Style = Format.AsNameOrTitle(newStyle, Format.CapitalsOptions.EachWord);
                Console.WriteLine("Would you like to edit the alcohol volume? (Confirming blank space will proceed without change.)");
                string newABV = Console.ReadLine();
                beersResult[0].AlcoholByVolume = Format.AsScoreOrABV(newABV, 94.99);
                Console.WriteLine("Would you like to edit the flavor? (Confirming blank space will proceed without change.)");
                var newFlavors = Format.AsTags(Console.ReadLine());
                beersResult[0].Flavors = newFlavors;
                Console.WriteLine("Would you like to edit the score? (Confirming blank space will proceed without change.)");
                string newScore = Console.ReadLine();
                beersResult[0].Score = Format.AsScoreOrABV(newScore, 10);
                return;
            }
            Console.WriteLine("The entered name is invalid. Please enter a proper name of a beer.");
            Console.ReadKey();
            Console.CursorVisible = false;
        }
    }
}
