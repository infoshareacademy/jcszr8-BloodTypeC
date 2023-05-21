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
        public async static Task Initialize(BeeropediaContext context, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            await context.Database.EnsureCreatedAsync();
            //Seeding beers to DB
            if (context.AllBeers.Any())
            {
                return;   // DB has been seeded
            }

            await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            var serviceAdmin = new User { Email = "artur@example.com", UserName = "artur@example.com" };
            await userManager.CreateAsync(serviceAdmin, "somePasssadsad123@");
            await userManager.AddToRoleAsync(serviceAdmin, "Admin");

            var beersToSeed = await JsonSerializer.DeserializeAsync<List<Beer>>(new MemoryStream(Resources.beers), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            foreach (var beer in beersToSeed)
            {
                beer.Added = DateTime.Now;
                //beer.LastModified = DateTime.Now;
                await context.AllBeers.AddAsync(beer);
                await context.SaveChangesAsync();
            }
        }
    }
}
