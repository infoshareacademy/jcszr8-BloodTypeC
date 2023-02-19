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
                CWS.ColoredMsg("Press any key to exit!", "blue");
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
                        CWS.ColoredMsg("SEARCH BEER\n===========\n" +
                            "What to look for?\n", "yellow");
                        string beerNameForSearch = CWS.ReadLine();
                        if (!string.IsNullOrWhiteSpace(beerNameForSearch))
                        {
                            beersResult = BeerOperations.SearchByName(DB.AllBeers, beerNameForSearch);
                            BeerOperations.DisplayBeer(beersResult, true);
                            break;
                        }
                        CWS.ColoredMsg("The search was cancelled.", "red");
                        CWS.ColoredMsg("Returning to menu.", "blue");
                        Console.ReadKey();
                        break;
                    case 1: // Search by brewery
                        Console.Clear();
                        CWS.ColoredMsg("SEARCH BREWERY\n==============\n" +
                            "What to look for?\n", "yellow");
                        string breweryNameForSearch = CWS.ReadLine();
                        if (!string.IsNullOrWhiteSpace(breweryNameForSearch))
                        {
                            beersResult = BeerOperations.SearchByBrewery(DB.AllBeers, breweryNameForSearch);
                            BeerOperations.DisplayBeer(beersResult,true);
                            break;
                        }
                        CWS.ColoredMsg("The search was cancelled.\n", "red");
                        CWS.ColoredMsg("Returning to menu.", "blue");
                        Console.ReadKey();
                        break;
                    case 2: // Search by style
                        Console.Clear();
                        CWS.ColoredMsg("SEARCH BY STYLE\n===============\n" +
                            "What to look for?\n", "yellow");
                        string beerStyleForSearch = CWS.ReadLine();
                        if (!string.IsNullOrWhiteSpace(beerStyleForSearch))
                        {
                            beersResult = BeerOperations.SearchByStyle(DB.AllBeers, beerStyleForSearch);
                            BeerOperations.DisplayBeer(beersResult, true);
                            break;
                        }
                        CWS.ColoredMsg("The search was cancelled.\n", "red");
                        CWS.ColoredMsg("Returning to menu.", "blue");
                        Console.ReadKey();
                        break;

                    case 3: // Search by ABV
                        Console.Clear();
                        double minAbv, maxAbv;
                        CWS.ColoredMsg("SEARCH BY ABV\n=============\n" +
                            "Enter min ABV\n", "yellow");
                        if (!double.TryParse(CWS.ReadLine(), out minAbv))
                        {
                            CWS.ColoredMsg("Error. The entered data was not a number.\n", "red");
                            CWS.ColoredMsg("Returning to menu.", "blue");
                            Console.ReadKey();
                            break;
                        }
                        CWS.ColoredMsg("Enter max ABV\n", "yellow");
                        if (!double.TryParse(CWS.ReadLine(), out maxAbv))
                        {
                            CWS.ColoredMsg("Error. The entered data was not a number.\n", "red");
                            CWS.ColoredMsg("Returning to menu.", "blue");
                            Console.ReadKey();
                            break;
                        }
                        beersResult = BeerOperations.SearchByAlcVol(DB.AllBeers, minAbv, maxAbv);
                        BeerOperations.DisplayBeer(beersResult, true);
                        break;
                    case 4: // Search by flavor
                        Console.Clear();
                        CWS.ColoredMsg("SEARCH BY FLAVOR\n================\n" +
                            "Enter the flavor of the beer that you are looking for.\n", "yellow");
                        CWS.ColoredMsg("Flavors in our database:\n", "gray");
                        var flavors = DB.AllBeers.Where(x => x.Flavors != null).SelectMany(beer => beer.Flavors).Distinct().ToList();
                        Console.WriteLine(string.Join(", ", flavors));
                        string searchFlavor = CWS.ReadLine();
                        if (!string.IsNullOrWhiteSpace(searchFlavor))
                        {
                            beersResult = BeerOperations.SearchByFlavor(DB.AllBeers, searchFlavor);
                            BeerOperations.DisplayBeer(beersResult, true);
                            break;
                        }
                        CWS.ColoredMsg("The search was cancelled.\n", "red");
                        CWS.ColoredMsg("Returning to menu.", "blue");
                        Console.ReadKey();
                        break;
                    case 5:
                        BeerOperations.NewBeer();
                        break;
                    case 6:
                        BeerOperations.EditBeer();
                        break;
                    case 7:
                        Clear();
                        WriteLine($"{logo}\nThank you for coming! Have a nice day!");
                        CWS.ColoredMsg("Press any key to close the application!", "blue");
                        ReadKey(true);
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
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
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    beersResult[0].Name = newName;
                    beersResult[0].Name = Format.AsNameOrTitle(newName, Format.CapitalsOptions.FirstWord, true);
                }
                Console.WriteLine("Would you like to edit the brewery? (Confirming blank space will proceed without change.)");
                string newBrewery = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newBrewery))
                {
                    beersResult[0].Brewery = newBrewery;
                    beersResult[0].Brewery = Format.AsNameOrTitle(newBrewery, Format.CapitalsOptions.EachWord, true);
                }
                Console.WriteLine("Would you like to edit the style? (Confirming blank space will proceed without change.)");
                string newStyle = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newStyle))
                {
                    beersResult[0].Style = newStyle;
                    beersResult[0].Style = Regex.Replace(newStyle.ToLower(), "[^a-z ąćęłńóśżź-]", " ");
                    beersResult[0].Style = Regex.Replace(newStyle, @"\s+", " ");
                    beersResult[0].Style = Regex.Replace(newStyle, @"-+", "-");
                    beersResult[0].Style = Format.AsNameOrTitle(newStyle, Format.CapitalsOptions.EachWord, true);
                }
                Console.WriteLine("Would you like to edit the alcohol volume? (Confirming blank space will proceed without change.)");
                string newABV = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newABV))
                {
                    beersResult[0].AlcoholByVolume = Format.AsScoreOrABV(newABV, 94.99);
                }
                Console.WriteLine("Would you like to edit the flavor? (Confirming blank space will proceed without change.)");
                var newFlavors = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newFlavors))
                {
                    var newFlavorsFormated = Format.AsTags(newFlavors);
                    beersResult[0].Flavors = newFlavorsFormated;
                }               
                Console.WriteLine("Would you like to edit the score? (Confirming blank space will proceed without change.)");
                string newScore = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newScore))
                {
                    beersResult[0].Score = Format.AsScoreOrABV(newScore, 10);
                }                
                Console.WriteLine("Beer edited successfully. Press any key to return to main menu.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("The entered name is invalid. Try agian.");
            Console.ReadKey();
            Console.CursorVisible = false;
        }
    }
}
