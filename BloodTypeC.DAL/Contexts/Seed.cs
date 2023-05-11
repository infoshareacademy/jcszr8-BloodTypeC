using BloodTypeC.DAL.Models;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Identity;
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
        public static void Initialize(BeeropediaContext context)//, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            context.Database.EnsureCreated();
            //Seeding beers to DB
            if (context.AllBeers.Any())
            {
                return;   // DB has been seeded
            }

            //roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            //var serviceAdmin = new User { Email = "" };
            //userManager.CreateAsync(serviceAdmin);
            //userManager.AddToRoleAsync(serviceAdmin, "Admin");

            var beersToSeed = JsonSerializer.Deserialize<List<Beer>>(Resources.beers, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            foreach (var beer in beersToSeed)
            {
                beer.Added = DateTime.Now;
                //beer.LastModified = DateTime.Now;
                context.AllBeers.Add(beer);
                context.SaveChanges();
            }
        }
    }
}
