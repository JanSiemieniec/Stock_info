using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektAPBD.Server.Data.Migrations
{
    public partial class bringBackFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchList_AspNetUsers_ApplicationUserId",
                table: "WatchList");

            migrationBuilder.DropIndex(
                name: "IX_WatchList_ApplicationUserId",
                table: "WatchList");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "WatchList");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "WatchList",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WatchList_Id",
                table: "WatchList",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchList_AspNetUsers_Id",
                table: "WatchList",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchList_AspNetUsers_Id",
                table: "WatchList");

            migrationBuilder.DropIndex(
                name: "IX_WatchList_Id",
                table: "WatchList");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "WatchList",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "WatchList",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WatchList_ApplicationUserId",
                table: "WatchList",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchList_AspNetUsers_ApplicationUserId",
                table: "WatchList",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
