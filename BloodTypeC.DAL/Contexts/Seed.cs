using BloodTypeC.DAL.Models;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BloodTypeC.DAL.Contexts
{
    public static class Seed
    {
        public static void Initialize(BeeropediaContext context)
        {
            context.Database.EnsureCreated();
            //Seeding beers to DB
            if (context.AllBeers.Any())
            {
                return;   // DB has been seeded
            }

            var beersToSeed = JsonSerializer.Deserialize<List<Beer>>(Resources.beers, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            foreach (var beer in beersToSeed)
            {
                beer.Added = DateTime.Now;
                beer.LastModified = DateTime.Now;
                context.AllBeers.Add(beer);
                context.SaveChanges();
            }
            //Seeding flavors to DB
            if (context.AllFlavors.Any())
            {
                return;   // Flavors has been seeded
            }
            var flavorsToSeed = beersToSeed.Where(x => x.Flavors != null).SelectMany(beer => beer.Flavors).Distinct().ToList();
            
            foreach (var flavor in flavorsToSeed)
            {
                FlavorEntity flavorEntity = new();
                flavorEntity.Flavor = flavor;
                //flavorEntity.Flavor = flavor;
                context.AllFlavors.Add(flavorEntity);
                context.SaveChanges();
            }
        }
    }
}
