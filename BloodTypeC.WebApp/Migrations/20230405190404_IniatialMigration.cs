using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodTypeC.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class IniatialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllBeers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brewery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Flavors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlcoholByVolume = table.Column<double>(type: "float", nullable: true),
                    Score = table.Column<double>(type: "float", nullable: true),
                    Added = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllBeers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AllFlavors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Flavor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllFlavors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteBeers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteBeers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BeerBeerFavorites",
                columns: table => new
                {
                    BeerFavoritesId = table.Column<int>(type: "int", nullable: false),
                    BeersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerBeerFavorites", x => new { x.BeerFavoritesId, x.BeersId });
                    table.ForeignKey(
                        name: "FK_BeerBeerFavorites_AllBeers_BeersId",
                        column: x => x.BeersId,
                        principalTable: "AllBeers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeerBeerFavorites_FavoriteBeers_BeerFavoritesId",
                        column: x => x.BeerFavoritesId,
                        principalTable: "FavoriteBeers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeerBeerFavorites_BeersId",
                table: "BeerBeerFavorites",
                column: "BeersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllFlavors");

            migrationBuilder.DropTable(
                name: "BeerBeerFavorites");

            migrationBuilder.DropTable(
                name: "AllBeers");

            migrationBuilder.DropTable(
                name: "FavoriteBeers");
        }
    }
}
