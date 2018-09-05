using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class LocationWithUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flat_Location_LocationId",
                table: "Flat");

            migrationBuilder.DropIndex(
                name: "IX_Flat_LocationId",
                table: "Flat");

            migrationBuilder.AlterColumn<long>(
                name: "LocationId",
                table: "Flat",
                nullable: true,
                oldClrType: typeof(long));

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

            migrationBuilder.AlterColumn<long>(
                name: "LocationId",
                table: "Flat",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flat_LocationId",
                table: "Flat",
                column: "LocationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Flat_Location_LocationId",
                table: "Flat",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
