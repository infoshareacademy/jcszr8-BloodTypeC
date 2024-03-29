﻿using BloodTypeC.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

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
            var serviceAdmin = new User { Email = "admin@beeropedia.com", UserName = "admin@beeropedia.com", EmailConfirmed = true};
            await userManager.CreateAsync(serviceAdmin, "Qwer5^yhn");
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
