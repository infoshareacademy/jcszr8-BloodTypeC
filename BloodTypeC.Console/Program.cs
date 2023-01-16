using System;
using BloodTypeC.DAL;
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
            Console.CursorVisible = true;
            Navigation naviFirstMenu = new Navigation();
            naviFirstMenu.Start();
        }
    }
}
