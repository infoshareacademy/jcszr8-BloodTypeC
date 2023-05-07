﻿using BloodTypeC.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace BloodTypeC.WebApp.Migrations
{
    [DbContext(typeof(BeeropediaContext))]
    partial class BeeropediaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BeerBeerFavorites", b =>
                {
                    b.Property<int>("BeerFavoritesId")
                        .HasColumnType("int");

                    b.Property<string>("BeersId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("BeerFavoritesId", "BeersId");

                    b.HasIndex("BeersId");

                    b.ToTable("BeerBeerFavorites");
                });

            modelBuilder.Entity("BloodTypeC.DAL.BeerFavorites", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("FavoriteBeers");
                });

            modelBuilder.Entity("BloodTypeC.DAL.Models.Beer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasAnnotation("Relational:JsonPropertyName", "beer_id");

                    b.Property<DateTime>("Added")
                        .HasColumnType("datetime2");

                    b.Property<double?>("AlcoholByVolume")
                        .HasColumnType("float")
                        .HasAnnotation("Relational:JsonPropertyName", "abv");

                    b.Property<string>("Brewery")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Flavors")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Score")
                        .HasColumnType("float");

                    b.Property<string>("Style")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AllBeers");
                });

            modelBuilder.Entity("BeerBeerFavorites", b =>
                {
                    b.HasOne("BloodTypeC.DAL.BeerFavorites", null)
                        .WithMany()
                        .HasForeignKey("BeerFavoritesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BloodTypeC.DAL.Models.Beer", null)
                        .WithMany()
                        .HasForeignKey("BeersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
