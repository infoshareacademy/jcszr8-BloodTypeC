using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using BloodTypeC.Logic;
using BloodTypeC.DAL;

namespace BloodTypeC.ConsoleUI
{
    class Navigation
    {
        public void Start()
        {
            Title = "Beer-o-pedia";
            RunStartMenu();
        }
        private void RunStartMenu()
        {
            string prompt = @"
 ______   _______  _______  _______         _______         _______  _______  ______  _________ _______ 
(  ___ \ (  ____ \(  ____ \(  ____ )       (  ___  )       (  ____ )(  ____ \(  __  \ \__   __/(  ___  )
| (   ) )| (    \/| (    \/| (    )|       | (   ) |       | (    )|| (    \/| (  \  )   ) (   | (   ) |
| (__/ / | (__    | (__    | (____)| _____ | |   | | _____ | (____)|| (__    | |   ) |   | |   | (___) |
|  __ (  |  __)   |  __)   |     __)(_____)| |   | |(_____)|  _____)|  __)   | |   | |   | |   |  ___  |
| (  \ \ | (      | (      | (\ (          | |   | |       | (      | (      | |   ) |   | |   | (   ) |
| )___) )| (____/\| (____/\| ) \ \__       | (___) |       | )      | (____/\| (__/  )___) (___| )   ( |
|/ \___/ (_______/(_______/|/   \__/       (_______)       |/       (_______/(______/ \_______/|/     \|
                                                                                            
Witaj w Beer-o-pedii! Czy masz skończone 18 lat?";
            string[] options = { "Tak", "Nie", };
            Menu startMenu = new Menu(prompt, options);
            int selectedIndex = startMenu.Run();

            switch (selectedIndex)
            {
                case 0: Over18();
                    break;
                case 1: Not18();
                    break;
            }
        }
        private void Over18()
        {
            string prompt = @"
 ______   _______  _______  _______         _______         _______  _______  ______  _________ _______ 
(  ___ \ (  ____ \(  ____ \(  ____ )       (  ___  )       (  ____ )(  ____ \(  __  \ \__   __/(  ___  )
| (   ) )| (    \/| (    \/| (    )|       | (   ) |       | (    )|| (    \/| (  \  )   ) (   | (   ) |
| (__/ / | (__    | (__    | (____)| _____ | |   | | _____ | (____)|| (__    | |   ) |   | |   | (___) |
|  __ (  |  __)   |  __)   |     __)(_____)| |   | |(_____)|  _____)|  __)   | |   | |   | |   |  ___  |
| (  \ \ | (      | (      | (\ (          | |   | |       | (      | (      | |   ) |   | |   | (   ) |
| )___) )| (____/\| (____/\| ) \ \__       | (___) |       | )      | (____/\| (__/  )___) (___| )   ( |
|/ \___/ (_______/(_______/|/   \__/       (_______)       |/       (_______/(______/ \_______/|/     \|
                                                                                            
Witaj w menu głownym piwnej encyklopedii";
            string[] options = { "1. Wyszukiwanie piwa po nazwie.", "2. Wyszukiwanie piwa wg producenta (Browaru).",
            "3. Wyszukiwanie piwa wg gatunków.", "4. Wyszukiwanie piwa wg zawartości alkoholu.", "5. Wyszukiwanie piwa wg smaku.",
            "6. Wprowadzenie nowego piwa/browaru.", "7. Edycja istniejącego piwa/browaru.", "8. Zamknięcie aplikacji."};

            Load.LoadFromFile();
            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();
            var beersResult = new List<Beer>();
            switch (selectedIndex)
            {
                case 0: // Search name
                    Console.CursorVisible = true;
                    beersResult = BeerSearch.SearchByName(Beer.AllBeers, Console.ReadLine());
                    SearchName();
                    break;
                case 1: SearchBrewery();
                    break;
                case 2: SearchType();
                    break;
                case 3: SearchAlkVol();
                    break;
                case 4: SearchFlavor();
                    break;
                case 5: NewBeerBrewery();
                    break;
                case 6: EditBeerBrewery();
                    break;
                case 7: AppExit();
                    break;
            }

            // Display beersResult whatever it is. If its empty - display message about not found.
        }

        private void Not18()
        {
            Clear();
            ForegroundColor = ConsoleColor.Red;
            WriteLine(@"
 ______   _______  _______  _______         _______         _______  _______  ______  _________ _______ 
(  ___ \ (  ____ \(  ____ \(  ____ )       (  ___  )       (  ____ )(  ____ \(  __  \ \__   __/(  ___  )
| (   ) )| (    \/| (    \/| (    )|       | (   ) |       | (    )|| (    \/| (  \  )   ) (   | (   ) |
| (__/ / | (__    | (__    | (____)| _____ | |   | | _____ | (____)|| (__    | |   ) |   | |   | (___) |
|  __ (  |  __)   |  __)   |     __)(_____)| |   | |(_____)|  _____)|  __)   | |   | |   | |   |  ___  |
| (  \ \ | (      | (      | (\ (          | |   | |       | (      | (      | |   ) |   | |   | (   ) |
| )___) )| (____/\| (____/\| ) \ \__       | (___) |       | )      | (____/\| (__/  )___) (___| )   ( |
|/ \___/ (_______/(_______/|/   \__/       (_______)       |/       (_______/(______/ \_______/|/     \|)");
            ForegroundColor = ConsoleColor.Red;
            WriteLine("Uciekaj niegrzeczniuszku, bo dostaniesz klapsa!");
            WriteLine("Wciśnij dowolny klawisz aby zamkąć aplikację!");
            ResetColor();
            ReadKey(true);
            Environment.Exit(0);
        }
        private void SearchName()
        {
            Console.CursorVisible = true;
            
            //foreach (var item in )
            //{
            //    Console.WriteLine($"{item.Name} | {item.Brewery}");
            //}
            Console.ReadKey();

        }
        private void SearchBrewery()
        {

        }
        private void SearchType()
        {

        }
        private void SearchAlkVol()
        {

        }
        private void SearchFlavor()
        {

        }
        private void NewBeerBrewery()
        {

        }
        private void EditBeerBrewery()
        {

        }
        private void AppExit()
        {
            Clear();
            WriteLine(@"
 ______   _______  _______  _______         _______         _______  _______  ______  _________ _______ 
(  ___ \ (  ____ \(  ____ \(  ____ )       (  ___  )       (  ____ )(  ____ \(  __  \ \__   __/(  ___  )
| (   ) )| (    \/| (    \/| (    )|       | (   ) |       | (    )|| (    \/| (  \  )   ) (   | (   ) |
| (__/ / | (__    | (__    | (____)| _____ | |   | | _____ | (____)|| (__    | |   ) |   | |   | (___) |
|  __ (  |  __)   |  __)   |     __)(_____)| |   | |(_____)|  _____)|  __)   | |   | |   | |   |  ___  |
| (  \ \ | (      | (      | (\ (          | |   | |       | (      | (      | |   ) |   | |   | (   ) |
| )___) )| (____/\| (____/\| ) \ \__       | (___) |       | )      | (____/\| (__/  )___) (___| )   ( |
|/ \___/ (_______/(_______/|/   \__/       (_______)       |/       (_______/(______/ \_______/|/     \|)");
            WriteLine("Dziekujemy za Twoja wizytę. Miłego dnia!");
            WriteLine("Wciśnij dowolny klawisz aby zamkąć aplikację!");
            ReadKey(true);
            Environment.Exit(0);
        }
    }
}
