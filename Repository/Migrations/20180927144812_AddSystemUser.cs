using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AddSystemUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "IF (NOT EXISTS(SELECT * FROM [User] WHERE Name = \'system\'))" +
                " BEGIN" +
                " INSERT INTO [User](Name, Email, CreatedDate, Role, PhoneNumber, CreatedBy, Surname, Password)" +
                " VALUES(\'system\', \'system@admin.com\',SYSUTCDATETIME(), 1, \'System\', 0, \'system\', \'system\')" +
                " END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
