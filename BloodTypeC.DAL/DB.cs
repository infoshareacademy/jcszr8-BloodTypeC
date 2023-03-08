using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypeC.DAL
{
    public class DB
    {
        public static List<Beer> AllBeers { get; set; }
        public static List<Beer>? Favorites { get; set; } = new List<Beer>();
        public static List<string> AllFlavors { get; set; } = new List<string>();
    }
}
