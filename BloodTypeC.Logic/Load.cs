using BloodTypeC.DAL;
using BloodTypeC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace BloodTypeC.Logic
{
    public class Load
    {
        public static void LoadFromFile()
        {
            Beer.allBeers = JsonSerializer.Deserialize<List<Beer>>(BloodTypeC.DAL.Resources.beers);
        }
    }
}
