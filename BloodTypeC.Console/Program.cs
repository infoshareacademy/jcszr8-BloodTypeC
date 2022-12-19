using System;
using BloodTypeC.Logic;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading.Channels;

namespace BloodTypeC.ConsoleUI // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Czy masz 18 lat?");
            (int left, int top) = Console.GetCursorPosition();
            var option = 1;
            var decorator = "\u001b[31m";
            ConsoleKeyInfo key;
            bool isSelected = false;

            while (!isSelected)
            {
                Console.SetCursorPosition(left, top);

                Console.WriteLine($"{(option == 1 ? decorator : "")}Tak\u001b[0m");
                Console.WriteLine($"{(option == 2 ? decorator : "")}Nie\u001b[0m");

                key = Console.ReadKey(false);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        option = option == 1 ? 2 : option - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        option = option == 2 ? 1 : option + 1;
                        break;

                    case ConsoleKey.Enter:
                        isSelected = true;
                        if (option == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("otwiera się menu");
                        }
                        if (option == 2)
                        {
                            Console.Clear();
                        }
                        break;
                }
            }

           
        }

    }
}