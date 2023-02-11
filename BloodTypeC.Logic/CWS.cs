using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypeC.ConsoleUI
{
    public class CWS
    {
        public static void ColoredMsg(string message, string color)
        {
            if (color.ToLower().Contains("dark"))
            {
                color = "Dark" + char.ToUpper(color[4]) + color.Substring(5);
            }
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), char.ToUpper(color[0]) + color.Substring(1));
            Console.Write(message);
            Console.ResetColor();
        }

        public static string ReadLine()
        {
            Console.CursorVisible = true;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            string input = Console.ReadLine();
            Console.ResetColor();
            Console.CursorVisible = false;
            return input;
        }
    }
}
