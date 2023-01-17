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
                                "3. Wyszukiwanie piwa wg gatunków.", "4. Wyszukiwanie piwa wg zawartości alkoholu.",
                                "5. Wyszukiwanie piwa wg smaku.", "6. Wprowadzenie nowego piwa/browaru.", "7. Edycja istniejącego piwa/browaru.",
                                "8. Zamknięcie aplikacji."};

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
                        NewBeerBrewery();
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
            Console.Clear();
            Console.CursorVisible = true;
            Beer beerToAdd = new Beer();

            Console.WriteLine("\nTo add a beer to our database, first we need the name of the beer.");
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
                Console.WriteLine("Sure, the style can be unknown.");
            }
            beerToAdd.Style = input;

            Console.WriteLine("\nWhat can be tasted in this beer? Use commas (,) and/or spaces when adding multiple flavours.");
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
                Console.WriteLine("Fine, the flavours remain to be disovered.");
            }
            var flavors = Format.AsTags(input);
            beerToAdd.Flavors = flavors;
            beerToAdd.Add();

            Console.CursorVisible = false;
        }
        private void EditBeerBrewery()
        {

        }
    }      
}
