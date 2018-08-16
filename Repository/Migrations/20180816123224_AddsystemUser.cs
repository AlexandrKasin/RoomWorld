using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AddsystemUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                table: "User",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "User",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ModifiedBy",
                table: "User",
                nullable: true);

            migrationBuilder.Sql(
                "IF (NOT EXISTS(SELECT * FROM [User] WHERE Name = \'system\'))" +
                " BEGIN" +
                " INSERT INTO [User](Name, Email, CreatedDate, Role)" +
                " VALUES(\'system\', \'system@admin.com\',SYSUTCDATETIME(), 1)" +
                " END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "User");
        }
    }
}
