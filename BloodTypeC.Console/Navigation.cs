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
                    case 0:
                        Console.CursorVisible = true;
                        Console.WriteLine("Podaj nazwę piwa, które chciałbyś znaleźć?");
                        var beerNameToSearch = Console.ReadLine();
                        if (!string.IsNullOrEmpty(beerNameToSearch))
                            beersResult = BeerSearch.SearchByName(DB.AllBeers, beerNameToSearch);
                            BeerSearch.DisplayBeer();
                        break;
                    case 1:
                        Console.CursorVisible = true;
                        Console.WriteLine("Podaj nazwę browaru (producenta), którego piwa chciałbyś zobaczyć?");
                        var breweryNameToSearch = Console.ReadLine();
                        if (!string.IsNullOrEmpty(breweryNameToSearch))
                            beersResult = BeerSearch.SearchByName(DB.AllBeers, breweryNameToSearch);
                        BeerSearch.DisplayBeer();
                        break;
                    case 2:
                        Console.CursorVisible = true;
                        Console.WriteLine("Podaj nazwę piwa po której chesz szukać");
                        var beerNameToSearch = Console.ReadLine();
                        if (!string.IsNullOrEmpty(beerNameToSearch))
                            beersResult = BeerSearch.SearchByName(DB.AllBeers, beerNameToSearch);
                        BeerSearch.DisplayBeer();
                        break;
                    case 3:
                        Console.CursorVisible = true;
                        Console.WriteLine("Podaj nazwę piwa po której chesz szukać");
                        var beerNameToSearch = Console.ReadLine();
                        if (!string.IsNullOrEmpty(beerNameToSearch))
                            beersResult = BeerSearch.SearchByName(DB.AllBeers, beerNameToSearch);
                        BeerSearch.DisplayBeer();
                        break;
                    case 4:
                        Console.CursorVisible = true;
                        Console.WriteLine("Podaj nazwę piwa po której chesz szukać");
                        var beerNameToSearch = Console.ReadLine();
                        if (!string.IsNullOrEmpty(beerNameToSearch))
                            beersResult = BeerSearch.SearchByName(DB.AllBeers, beerNameToSearch);
                        BeerSearch.DisplayBeer();
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
                }
                break;
                }
            }
        private void NewBeerBrewery()
        {

        }
        private void EditBeerBrewery()
        {

        }
    }      
}
