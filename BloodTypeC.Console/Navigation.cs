using BloodTypeC.DAL;
using BloodTypeC.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
            while (true)
            {
                Console.CursorVisible = true;
                Load.LoadFromFile();
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
                        //
                        Console.Clear();
                        Console.WriteLine("Enter the name of the beer that you are looking for:");
                        string beerNameForSearch = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(beerNameForSearch))
                        {
                            beersResult = BeerSearch.SearchByName(DB.AllBeers, beerNameForSearch);
                            BeerSearch.DisplayBeer(beersResult);
                            break;
                        }
                        Console.WriteLine("The entered name is invalid. Please enter a proper name of a beer.");
                        Console.ReadKey();
                        break;
                    case 1: // Search by brewery
                        Console.Clear();
                        Console.WriteLine("Enter the brewery name of the beer that you are looking for:");
                        string breweryNameForSearch = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(breweryNameForSearch))
                        {
                            beersResult = BeerSearch.SearchByBrewery(DB.AllBeers, breweryNameForSearch);
                            BeerSearch.DisplayBeer(beersResult);
                            break;
                        }
                        Console.WriteLine("The entered brewery name is invalid. Please enter a proper name of a beer.");
                        Console.ReadKey();
                        break;
                    case 2: // Search by style
                        Console.Clear();
                        Console.WriteLine("Tu będzie szukanie po stylu/typie");
                        Console.ReadKey();
                        break;
                    case 3: // Search by ABV
                        Console.Clear();
                        double minAbv,maxAbv;

                        Console.WriteLine("Enter min ABV");
                        if(!double.TryParse(Console.ReadLine(), out minAbv)) 
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
                        BeerSearch.DisplayBeer(beersResult);
                        break;
                    case 4: // Search by flavors
                        Console.Clear();
                        Console.WriteLine("Tu będzie szukanie po smaku");
                        Console.ReadKey();
                        break;
                    case 5:
                        NewBeer();
                        break;
                    case 6:

                        break;
                    case 7:
                        Clear();
                        WriteLine($"{logo}Dziekujemy za Twoja wizytę. Miłego dnia!");
                        WriteLine("Wciśnij dowolny klawisz aby zamkąć aplikację!");
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

            Console.WriteLine("\nTo add a beer, first we need the name of the beer.");
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Sorry. We cannot add a beer without a name.");
                Console.ReadKey(true);
                Console.CursorVisible = false;
                return;
            }
            beerToAdd.Name = Format.AsNameOrTitle(input, Format.CapitalsOptions.EachWord);

            Console.WriteLine("\nNow tell us the name of the brewery.");
            input = Console.ReadLine() ?? "Unknown";
            if (input == "Unknown")
            {
                Console.WriteLine("Ok, brewery is unknown.");
            }
            beerToAdd.Brewery = Format.AsNameOrTitle(input, Format.CapitalsOptions.FirstWord);

            Console.Write("\nWhat style is it?\n(add new or choose from:");
            string[] stylesFromDB = DB.AllBeers.Select(x => x.Style).ToArray();
            foreach (string style in stylesFromDB)
            {
                string separator = ",";
                if (style == stylesFromDB.Last())
                {
                    separator = "";
                }
                Console.Write($" {style}{separator}");
            }
            Console.Write(")\n");
            input = Console.ReadLine() ?? "Unknown";
            if (input == "Unknown")
            {
                Console.WriteLine("Sure, the style can be unknown, why not.");
            }
            beerToAdd.Style = input;

            Console.WriteLine("\nWhat can be tasted in this beer? Use commas (,) and/or spaces when adding multiple flavors.");
            Console.WriteLine("With other beers, fellow tasters have reported experiencing:\n [");
            string[] flavorsFromDB = DB.AllBeers.Select(x => x.Style).ToArray();
            foreach (string style in stylesFromDB)
            {
                string separator = ",";
                if (style == stylesFromDB.Last())
                {
                    separator = "";
                }
                Console.Write($" {style}{separator}");
            }
            Console.WriteLine("]");
            input = Console.ReadLine() ?? "Unknown";
            if (input == "Unknown")
            {
                Console.WriteLine("Fine, the flavors remain to be disovered.");
            }
            var flavors = Format.AsTags(input);
            beerToAdd.Flavors = flavors;

            Console.WriteLine("\nWhat is your score for this beer? 1-10");
            input = Console.ReadLine();
            if (input == null)
            {
                Console.WriteLine("No score, understood.");
            }
            beerToAdd.Score = Format.AsScore(input);

            beerToAdd.Add();

            Console.CursorVisible = false;
        }
        private void EditBeer()
        {

        }
    }      
}
