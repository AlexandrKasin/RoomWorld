using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class LocationConWithUserOneToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Flat_FlatId",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Location_FlatId",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "FlatId",
                table: "Location");

            migrationBuilder.AddColumn<long>(
                name: "LocationId",
                table: "Flat",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flat_LocationId",
                table: "Flat",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flat_Location_LocationId",
                table: "Flat",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flat_Location_LocationId",
                table: "Flat");

            migrationBuilder.DropIndex(
                name: "IX_Flat_LocationId",
                table: "Flat");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Flat");

            migrationBuilder.AddColumn<long>(
                name: "FlatId",
                table: "Location",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_FlatId",
                table: "Location",
                column: "FlatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Flat_FlatId",
                table: "Location",
                column: "FlatId",
                principalTable: "Flat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
