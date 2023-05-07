using BloodTypeC.DAL.Contexts;
using BloodTypeC.DAL.Repository;
using BloodTypeC.Logic;
using BloodTypeC.Logic.Services;
using BloodTypeC.Logic.Services.IServices;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BloodTypeC.DAL.Models;

namespace BloodTypeC.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                        var connectionString = builder.Configuration.GetConnectionString("BloodTypeCWebAppContextConnection") ?? throw new InvalidOperationException("Connection string 'BloodTypeCWebAppContextConnection' not found.");

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IBeerServices, BeerServices>();
            builder.Services.AddScoped<IBeerSearchServices, BeerSearchServices>();
            builder.Services.AddTransient<IFavoriteBeersServices, FavoriteBeersServices>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddDbContext<BeeropediaContext>();
                        builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<BeeropediaContext>();

            var app = builder.Build();
            CreateDbIfNotExists(app);
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = new List<CultureInfo> { new("en-US")},
                SupportedUICultures = new List<CultureInfo> { new("en-US")}
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
                        app.UseAuthentication();;

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
        private static void CreateDbIfNotExists(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<BeeropediaContext>();
                Seed.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }
    }
}