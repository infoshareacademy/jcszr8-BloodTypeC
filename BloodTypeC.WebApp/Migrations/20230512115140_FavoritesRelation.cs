using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodTypeC.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class FavoritesRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeerBeerFavorites");

            migrationBuilder.DropTable(
                name: "FavoriteBeers");

            migrationBuilder.CreateTable(
                name: "BeerUser",
                columns: table => new
                {
                    FavoriteBeersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FavoriteUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerUser", x => new { x.FavoriteBeersId, x.FavoriteUsersId });
                    table.ForeignKey(
                        name: "FK_BeerUser_AllBeers_FavoriteBeersId",
                        column: x => x.FavoriteBeersId,
                        principalTable: "AllBeers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeerUser_AspNetUsers_FavoriteUsersId",
                        column: x => x.FavoriteUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeerUser_FavoriteUsersId",
                table: "BeerUser",
                column: "FavoriteUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeerUser");

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
    }
}
