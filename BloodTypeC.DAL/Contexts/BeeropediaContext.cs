using BloodTypeC.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BloodTypeC.DAL.Contexts
{
    public class BeeropediaContext : IdentityDbContext<User, IdentityRole, string>
    {
        public DbSet<Beer> AllBeers { get; set;}
        public BeeropediaContext(DbContextOptions<BeeropediaContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=BloodTypeC;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", b => b.MigrationsAssembly("BloodTypeC.WebApp"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Beer>()
                .HasMany(b => b.FavoriteUsers)
                .WithMany(u => u.FavoriteBeers)
                .UsingEntity(e => e.ToTable("BeerUser"));
            
            modelBuilder.Entity<Beer>()
                .Property(f=>f.Flavors)
                .HasConversion(v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
            
            var valueComparer = new ValueComparer<List<string>>((c1, c2) => c1.SequenceEqual(c2), c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),c => c.ToList());
            
            modelBuilder.Entity<Beer>()
                .Property(f => f.Flavors)
                .Metadata
                .SetValueComparer(valueComparer);

            modelBuilder.Entity<Beer>()
                .HasMany(beer => beer.FavoriteUsers).WithMany(user => user.FavoriteBeers);

            modelBuilder.Entity<User>()
                .Property(prop => prop.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
