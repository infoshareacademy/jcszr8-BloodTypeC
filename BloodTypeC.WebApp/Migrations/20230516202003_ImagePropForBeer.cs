using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodTypeC.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class ImagePropForBeer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "AllBeers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "AllBeers");
        }
    }
}
