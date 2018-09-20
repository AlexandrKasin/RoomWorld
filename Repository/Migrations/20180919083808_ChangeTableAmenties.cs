using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class ChangeTableAmenties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Amentiese");

            migrationBuilder.DropColumn(
                name: "Availability",
                table: "Amentiese");

            migrationBuilder.RenameColumn(
                name: "Icon",
                table: "Amentiese",
                newName: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Amentiese",
                newName: "Icon");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Amentiese",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Availability",
                table: "Amentiese",
                nullable: false,
                defaultValue: false);
        }
    }
}
