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
                    case 5: // Adding a new beer
                        Console.Clear();
                        Console.CursorVisible = true;
                        Beer beerToAdd = new Beer();

                        // Name
                        CWS.ColoredMsg("ADDING A BEER\n=============\n", "yellow");
                        CWS.ColoredMsg("> To add a beer, first we need the name of the beer.\n", "yellow");
                        string input = CWS.ReadLine();
                        if (string.IsNullOrWhiteSpace(input))
                        {
                            CWS.ColoredMsg("Sorry. We cannot add a beer without a name.\n", "red");
                            CWS.ColoredMsg("Press any key to return to main menu.", "blue");
                            Console.ReadKey(true);
                            return;
                        }
                        beerToAdd.Name = Format.AsNameOrTitle(input, Format.CapitalsOptions.EachWord, false);

                        // Brewery
                        CWS.ColoredMsg("\n> Now tell us the name of the brewery.\n", "yellow");
                        input = CWS.ReadLine();
                        if (string.IsNullOrWhiteSpace(input))
                        {
                            CWS.ColoredMsg("Ok, the brewery is a mystery.\n", "darkyellow");
                        }
                        beerToAdd.Brewery = Format.AsNameOrTitle(input, Format.CapitalsOptions.EachWord, false);

                        //Style
                        CWS.ColoredMsg("\n> What style is it?\n", "yellow");
                        CWS.ColoredMsg("(add new or choose from: ", "gray");
                        string[] stylesFromDB = DB.AllBeers.Where(x => x.Style != null).Select(x => x.Style).Distinct().ToArray();
                        CWS.ColoredMsg((string.Join(", ", stylesFromDB)), "gray");
                        CWS.ColoredMsg(")\n", "gray");

                        input = CWS.ReadLine();
                        string reformattedInput = Format.AsNameOrTitle(input, Format.CapitalsOptions.EachWord, true);
                        if (!string.IsNullOrWhiteSpace(reformattedInput))
                        {
                            beerToAdd.Style = reformattedInput;
                        }
                        else
                        {
                            if (reformattedInput != input)
                            {
                                CWS.ColoredMsg("This style format is not accepted.\n", "darkred");
                            }
                            else
                            {
                                CWS.ColoredMsg("Sure, the style can be unknown, why not.\n", "darkyellow");
                            }
                        }

                        // Flavors
                        CWS.ColoredMsg("\n> What is the beer's taste like?\n", "yellow");
                        CWS.ColoredMsg("   (You can add multiple flavors. Any special character will be considered as a separator.)\n", "gray");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("   Fellow tasters have reported other beers as:\n  [");
                        List<string> flavorsFromDB = DB.AllBeers.Where(x => x.Flavors != null).SelectMany(beer => beer.Flavors).Distinct().ToList();
                        Console.Write(string.Join(", ", flavorsFromDB));
                        Console.WriteLine("]");
                        Console.ResetColor();
                        input = CWS.ReadLine();
                        var flavorsToAdd = Format.AsTags(input);
                        if (flavorsToAdd.Count > 0)
                        {
                            beerToAdd.Flavors = flavorsToAdd;
                        }
                        else if (!string.IsNullOrWhiteSpace(input) && flavorsToAdd.Count == 0)
                        {
                            CWS.ColoredMsg("The flavors are badly formatted.\n", "darkred");
                        }
                        else
                        {
                            CWS.ColoredMsg("Fine, the flavors remain to be disovered.\n", "darkyellow");
                        }

                        // Abv
                        CWS.ColoredMsg("\n> How much alcohol by volume does it have?\n", "yellow");
                        input = CWS.ReadLine();
                        if (string.IsNullOrWhiteSpace(input))
                        {
                            CWS.ColoredMsg("That's key info we are missing...\n", "darkyellow");
                        }
                        beerToAdd.AlcoholByVolume = Format.AsScoreOrABV(input, 94.99);
                        CWS.ColoredMsg($"We will set the abv to {beerToAdd.AlcoholByVolume}%.\n", "gray");

                        // Score
                        CWS.ColoredMsg("\n> What is your score for this beer? 1-10\n", "yellow");
                        input = CWS.ReadLine();
                        if (string.IsNullOrWhiteSpace(input))
                        {
                            CWS.ColoredMsg("No score, understood.\n", "darkyellow");
                        }
                        beerToAdd.Score = Format.AsScoreOrABV(input, 10);
                        CWS.ColoredMsg($"We will set the score to {beerToAdd.Score} then.\n", "gray");
                        Console.ForegroundColor = ConsoleColor.Green;
                        BeerOperations.AddBeer(beerToAdd);
                        Console.WriteLine($"\nSuccessfully added {beerToAdd.Name}!");
                        Console.ResetColor();
                        Console.ReadKey(true);
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
        }
    }
}
