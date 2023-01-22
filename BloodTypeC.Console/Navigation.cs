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
                            beersResult = BeerOperations.SearchByName(DB.AllBeers, beerNameForSearch);
                            BeerOperations.DisplayBeer(beersResult, true);
                            break;
                        }
                        Console.WriteLine("The entered beer name is invalid. Please enter a proper name of a beer.");
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
                            beersResult = BeerOperations.SearchByBrewery(DB.AllBeers, breweryNameForSearch);
                            BeerOperations.DisplayBeer(beersResult,true);
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
                            beersResult = BeerOperations.SearchByStyle(DB.AllBeers, beerStyleForSearch);
                            BeerOperations.DisplayBeer(beersResult, true);
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
                        beersResult = BeerOperations.SearchByAlcVol(DB.AllBeers, minAbv, maxAbv);
                        Console.Clear();
                        BeerOperations.DisplayBeer(beersResult, true);
                        Console.CursorVisible = false;
                        break;
                    case 4: // Search by flavors
                        Console.Clear();
                        Console.CursorVisible = true;
                        Console.WriteLine("Enter the flavor of the beer that you are looking for? Flavors in pedia:");
                        var flavors = DB.AllBeers.Where(x => x.Flavors != null).SelectMany(beer => beer.Flavors).Distinct().ToList();
                        Console.WriteLine(string.Join(", ", flavors));
                        string searchFlavor = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(searchFlavor))
                        {
                            beersResult = BeerOperations.SearchByFlavor(DB.AllBeers, searchFlavor);
                            BeerOperations.DisplayBeer(beersResult, true);
                            break;
                        }
                        Console.WriteLine("Entered flavor is null or whitespace. Please enter proper flavor of beer.");
                        Console.ReadKey();
                        Console.CursorVisible = false;
                        break;
                    case 5:
                        BeerOperations.NewBeer();
                        break;
                    case 6:
                        BeerOperations.EditBeer();
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
    }
}
