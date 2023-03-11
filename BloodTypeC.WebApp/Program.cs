using BloodTypeC.DAL;
using BloodTypeC.Logic;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BloodTypeC.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Load.LoadFromFile();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action}/{id?}",
                defaults: new
                {
                    controller = "Home", // default controller
                    action = "AgeCheck" // default action on the controller
                }
                );
            app.Run();   
        }
    }
}