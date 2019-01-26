using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class ChangeHouseRulesNameMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseRulese_Apartments_ApartmentId",
                table: "HouseRulese");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HouseRulese",
                table: "HouseRulese");

            migrationBuilder.RenameTable(
                name: "HouseRulese",
                newName: "RulesOfResidence");

            migrationBuilder.RenameIndex(
                name: "IX_HouseRulese_ApartmentId",
                table: "RulesOfResidence",
                newName: "IX_RulesOfResidence_ApartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RulesOfResidence",
                table: "RulesOfResidence",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RulesOfResidence_Apartments_ApartmentId",
                table: "RulesOfResidence",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RulesOfResidence_Apartments_ApartmentId",
                table: "RulesOfResidence");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RulesOfResidence",
                table: "RulesOfResidence");

            migrationBuilder.RenameTable(
                name: "RulesOfResidence",
                newName: "HouseRulese");

            migrationBuilder.RenameIndex(
                name: "IX_RulesOfResidence_ApartmentId",
                table: "HouseRulese",
                newName: "IX_HouseRulese_ApartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HouseRulese",
                table: "HouseRulese",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HouseRulese_Apartments_ApartmentId",
                table: "HouseRulese",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
