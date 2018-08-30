using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AddFieldUserForFlat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Flat",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flat_UserId",
                table: "Flat",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flat_User_UserId",
                table: "Flat",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flat_User_UserId",
                table: "Flat");

            migrationBuilder.DropIndex(
                name: "IX_Flat_UserId",
                table: "Flat");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Flat");
        }
    }
}
