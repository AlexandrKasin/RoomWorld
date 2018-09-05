using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class LocationCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sity",
                table: "Location",
                newName: "City");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sity",
                table: "Location",
                newName: "City");
        }
    }
}
