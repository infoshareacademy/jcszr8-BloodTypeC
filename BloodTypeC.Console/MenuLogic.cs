using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console; 

namespace BloodTypeC.ConsoleUI
{
    public class MenuLogic
    {
        private int SelectedIndex;
        private string[] Options;
        private string Prompt;

        public MenuLogic(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedIndex = 0;    
        }

        public void DisplayOptions()
        {
            WriteLine(Prompt);
            for (int i = 0 ; i < Options.Length; i++) 
            {
                string currentOption = Options[i];
                WriteLine($"<<{currentOption}>>");
            }
        }
    }
}
