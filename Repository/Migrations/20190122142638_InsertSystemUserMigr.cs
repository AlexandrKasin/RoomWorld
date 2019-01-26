using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class InsertSystemUserMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "IF (NOT EXISTS(SELECT * FROM [User] WHERE Name = \'system\'))" +
                " BEGIN" +
                " INSERT INTO [User](Name, Username, Email, CreatedDate, PhoneNumber, CreatedBy, Surname, Password)" +
                " VALUES(\'system\',\'system\', \'system@roomworld.com\',SYSUTCDATETIME(),  \'+1112233444\', 0, \'system\', \'system\')" +
                " END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
