using BloodTypeC.DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypeC.DAL.Contexts
{
    public class BeeropediaContext : DbContext
    {
        public DbSet<Beer> AllBeers { get; set;}
        public DbSet<BeerFavorites> FavoriteBeers { get; set;}
        public DbSet<FlavorEntity> AllFlavors { get; set;}

        public BeeropediaContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=BloodTypeC;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", b => b.MigrationsAssembly("BloodTypeC.WebApp"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beer>()
                .Property(f=>f.Flavors)
                .HasConversion(v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
            
            var valueComparer = new ValueComparer<List<string>>((c1, c2) => c1.SequenceEqual(c2), c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),c => c.ToList());
            
            modelBuilder.Entity<Beer>()
                .Property(f => f.Flavors)
                .Metadata
                .SetValueComparer(valueComparer);

            modelBuilder.Entity<BeerFavorites>()
                .HasMany(b=>b.Beers)
                .WithMany();

            modelBuilder.Entity<FlavorEntity>();
        }
    }
}
