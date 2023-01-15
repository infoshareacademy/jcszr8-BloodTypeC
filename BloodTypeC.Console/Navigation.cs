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
            string prompt = $"{logo}Witaj w Beer-o-pedii! Czy masz skończone 18 lat?";
            string[] options = { "Tak", "Nie" };
            Menu startMenu = new Menu(prompt, options);
            int selectedIndex = startMenu.Run();

            if (selectedIndex == 1)
            {
                Clear();
                ForegroundColor = ConsoleColor.Red;
                WriteLine($"{logo}Uciekaj niegrzeczniuszku, bo dostaniesz klapsa!");
                WriteLine("Wciśnij dowolny klawisz aby zamkąć aplikację!");
                ResetColor();
                ReadKey(true);
                Environment.Exit(0);
            }
            while (true)
            {
                prompt = $"{logo}Witaj w menu głownym piwnej encyklopedii";
                string[] options2 = { "1. Wyszukiwanie piwa po nazwie.", "2. Wyszukiwanie piwa wg producenta (Browaru).",
            "3. Wyszukiwanie piwa wg gatunków.", "4. Wyszukiwanie piwa wg zawartości alkoholu.", "5. Wyszukiwanie piwa wg smaku.",
            "6. Wprowadzenie nowego piwa/browaru.", "7. Edycja istniejącego piwa/browaru.", "8. Zamknięcie aplikacji."};

                Load.LoadFromFile();
                Menu mainMenu = new Menu(prompt, options2);
                selectedIndex = mainMenu.Run();
                var beersResult = new List<Beer>();
                switch (selectedIndex)
                {
                    case 0: // Search name
                        Console.CursorVisible = true;
                        beersResult = BeerSearch.SearchByName(DB.AllBeers, Console.ReadLine());
                        
                        break;
                    case 1:
                        
                        break;
                    case 2:
                       
                        break;
                    case 3:
                     
                        break;
                    case 4:
                       
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
