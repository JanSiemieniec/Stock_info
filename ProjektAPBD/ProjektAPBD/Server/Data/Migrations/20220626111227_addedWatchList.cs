using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektAPBD.Server.Data.Migrations
{
    public partial class addedWatchList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "WatchList",
                columns: table => new
                {
                    IdWatchList = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdStock = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchList", x => x.IdWatchList);
                    table.ForeignKey(
                        name: "FK_WatchList_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WatchList_Stock_IdStock",
                        column: x => x.IdStock,
                        principalTable: "Stock",
                        principalColumn: "IdStock",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WatchList_Id",
                table: "WatchList",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WatchList_IdStock",
                table: "WatchList",
                column: "IdStock");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchList");

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
