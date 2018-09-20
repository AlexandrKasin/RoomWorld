using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class TableHouseRules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HouseRulese",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    State = table.Column<bool>(nullable: false),
                    FlatId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseRulese", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseRulese_Flat_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HouseRulese_FlatId",
                table: "HouseRulese",
                column: "FlatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HouseRulese");
        }
    }
}
