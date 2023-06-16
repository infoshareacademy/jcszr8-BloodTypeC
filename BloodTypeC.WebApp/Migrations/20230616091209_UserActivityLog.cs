using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodTypeC.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class UserActivityLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddedByUserId",
                table: "AllBeers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserActivities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserAction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObjectName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserActivities_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllBeers_AddedByUserId",
                table: "AllBeers",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_UserId",
                table: "UserActivities",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AllBeers_AspNetUsers_AddedByUserId",
                table: "AllBeers",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllBeers_AspNetUsers_AddedByUserId",
                table: "AllBeers");

            migrationBuilder.DropTable(
                name: "UserActivities");

            migrationBuilder.DropIndex(
                name: "IX_AllBeers_AddedByUserId",
                table: "AllBeers");

            migrationBuilder.DropColumn(
                name: "AddedByUserId",
                table: "AllBeers");
        }
    }
}
