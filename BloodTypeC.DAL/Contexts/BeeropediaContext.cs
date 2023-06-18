using BloodTypeC.DAL.Models;
using BloodTypeC.DAL.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BloodTypeC.DAL.Contexts
{
    public class BeeropediaContext : IdentityDbContext<User, IdentityRole, string>
    {
        public DbSet<Beer> AllBeers { get; set;}
        public DbSet<UserActivity> UserActivities { get; set;}
        public DbSet<AdminReportsOptions> AdminReportsOptions { get; set;}
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
                    .UsingEntity(e => e.ToTable("BeerUser"))
                .HasOne(beer => beer.AddedByUser)
                    .WithMany(user => user.AddedBeers);
            
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
                .Property(a => a.Added)
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            modelBuilder.Entity<User>()
                .HasMany(user=>user.AddedBeers)
                .WithOne(beer=>beer.AddedByUser);
            
            modelBuilder.Entity<User>()
                .Property(prop => prop.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<UserActivity>()
                .Property(prop => prop.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<UserActivity>()
                .Property(e => e.UserAction)
                .HasConversion(new EnumToStringConverter<Enums.UserActions>());

            modelBuilder.Entity<User>()
                .HasMany(prop => prop.UserActivities)
                .WithOne(prop => prop.User);

            modelBuilder.Entity<AdminReportsOptions>()
                .HasKey(x=>x.Id);
        }
    }
}
