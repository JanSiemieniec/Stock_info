using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektAPBD.Server.Data.Migrations
{
    public partial class addedLogoToStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Stock");
        }
    }
}
