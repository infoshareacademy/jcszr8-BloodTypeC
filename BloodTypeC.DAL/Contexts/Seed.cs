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
                context.AllBeers.Add(beer);
            }
            //Seeding flavors to DB
            /*if (context.AllFlavors.Any())
            {
                return;   // Flavors has been seeded
            }
            var flavorsToSeed = beersToSeed.Where(x => x.Flavors != null).SelectMany(beer => beer.Flavors).Distinct().ToList();
            FlavorEntity flavorEntity = new();
            foreach (var flavor in flavorsToSeed)
            {
                flavorEntity.Flavor = flavor;
                context.AllFlavors.Add(flavorEntity);
            }*/
            context.SaveChanges();

            // todo: add rest;
        }
    }
}
