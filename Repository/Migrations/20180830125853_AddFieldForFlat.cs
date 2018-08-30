using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AddFieldForFlat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Numberflat",
                table: "Location",
                newName: "NumberFlat");

            migrationBuilder.AddColumn<int>(
                name: "Accommodates",
                table: "Flat",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Size",
                table: "Flat",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "SpaceOffered",
                table: "Flat",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accommodates",
                table: "Flat");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Flat");

            migrationBuilder.DropColumn(
                name: "SpaceOffered",
                table: "Flat");

            migrationBuilder.RenameColumn(
                name: "NumberFlat",
                table: "Location",
                newName: "Numberflat");
        }
    }
}
