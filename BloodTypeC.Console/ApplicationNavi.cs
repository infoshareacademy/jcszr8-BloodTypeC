using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace BloodTypeC.ConsoleUI
{
    class ApplicationNavi
    {
        public void Start()
        {
            string prompt = "Witaj w Beer-o-pedii! Czy masz skończone 18 lat?";
            string[] options = { "Tak", "Nie" };
            MenuLogic startMenu = new MenuLogic(prompt, options);
            startMenu.DisplayOptions();

            WriteLine("Press any key to exit...");
            ReadKey(true);
        }
    }
}
