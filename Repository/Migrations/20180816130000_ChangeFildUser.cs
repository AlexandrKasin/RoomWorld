using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class ChangeFildUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ModifiedBy",
                table: "User",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "User",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "User",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "User",
                nullable: true,
                oldClrType: typeof(long));
        }
    }
}
