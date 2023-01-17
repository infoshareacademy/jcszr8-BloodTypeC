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
                WriteLine($"{logo}Oh you are so naughty! Run or you will get spanked!");
                WriteLine("Press any key to exit!");
                ResetColor();
                ReadKey(true);
                Environment.Exit(0);
            }
            while (true)
            {
                prompt = $"{logo}Beer-o-pedia main menu!";
                string[] options2 = { "1. Search by beer name.", "2. Search by brewery.", "3. Search by style.", "4. Search by alkohol volume.", "5. Search by flavor.",
            "6. Adding new beer.", "7. Editing beer.", "8. Exit application."};

                Load.LoadFromFile();
                Menu mainMenu = new Menu(prompt, options2);
                selectedIndex = mainMenu.Run();
                var beersResult = new List<Beer>();
                switch (selectedIndex)
                {
                    case 0: // Search by name
                        //Console.CursorVisible = true;
                        Console.Clear();
                        Console.WriteLine("Enter the name of the beer that you are looking for:");                                                                     
                        string beerNameForSearch = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(beerNameForSearch))
                        {
                            beersResult = BeerSearch.SearchByName(DB.AllBeers, beerNameForSearch);
                            BeerSearch.DisplayBeer(beersResult);
                            break;
                        }
                        Console.WriteLine("Entered name is null or whitespace. Please enter proper name of beer.");
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
                        Console.WriteLine("Entered brewery name is null or whitespace. Please enter proper name of beer.");
                        Console.ReadKey();
                        break;
                    case 2: // Search by style
                        Console.Clear();
                        Console.WriteLine("Enter the style of the beer that you are looking for:");                    
                        string beerStyleForSearch = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(beerStyleForSearch))
                        {
                            beersResult = BeerSearch.SearchByStyle(DB.AllBeers, beerStyleForSearch);
                            BeerSearch.DisplayBeer(beersResult);
                            break;
                        }
                        Console.WriteLine("Entered style is null or whitespace. Please enter proper style of beer.");
                        Console.ReadKey();
                        break;
                        
                    case 3: // Search by ABV
                        Console.Clear();
                        double minAbv,maxAbv;
                        Console.WriteLine("Enter min ABV");
                        if(!double.TryParse(Console.ReadLine(), out minAbv)) 
                        {
                            Console.WriteLine("Error!Entered data was wrong!");
                            Console.ReadKey();
                            break;
                        }
                        Console.WriteLine("Enter max ABV");
                        if (!double.TryParse(Console.ReadLine(), out maxAbv))
                        {
                            Console.WriteLine("Error!Entered data was wrong!");
                            Console.ReadKey();
                            break;
                        }                       
                        beersResult = BeerSearch.SearchByAlkVol(DB.AllBeers,minAbv, maxAbv);
                        Console.Clear();
                        BeerSearch.DisplayBeer(beersResult);
                        break;
                    case 4: // Search by flavors
                        Console.Clear();
                        Console.WriteLine("Flavors in pedia:");
                        var flavors = DB.AllBeers.SelectMany(beer => beer.Flavors).Distinct().ToList();
                        Console.WriteLine(string.Join(", ", flavors));
                        string searchFlavor = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(searchFlavor))
                        {
                            beersResult = BeerSearch.SearchByFlavor(DB.AllBeers, searchFlavor);
                            BeerSearch.DisplayBeer(beersResult);
                            Console.ReadKey();
                        }
                        Console.WriteLine("Entered flavor is null or whitespace. Please enter proper flavor of beer.");
                        Console.ReadKey();
                        break;
                    case 5:

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
        private void NewBeerBrewery()
        {

        }
        private void EditBeery()
        {

        }
    }      
}
