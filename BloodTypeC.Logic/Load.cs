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
using BloodTypeC.DAL.Models;

namespace BloodTypeC.Logic
{
    public class Load
    {
        public static void LoadFromFile()
        {
            DB.AllBeers = JsonSerializer.Deserialize<List<Beer>>(Resources.beers, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
