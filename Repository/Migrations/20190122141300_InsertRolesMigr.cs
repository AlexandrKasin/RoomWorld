using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class InsertRolesMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "IF (NOT EXISTS(SELECT * FROM [Role] WHERE Name = \'user\'))" +
                " BEGIN" +
                " INSERT INTO [Role](Name, Description, CreatedDate, CreatedBy)" +
                " VALUES(\'user\', \'Users can rent or book your apartment.\',SYSUTCDATETIME(),0)" +
                " END");
            migrationBuilder.Sql(
                "IF (NOT EXISTS(SELECT * FROM [Role] WHERE Name = \'admin\'))" +
                " BEGIN" +
                " INSERT INTO [Role](Name, Description, CreatedDate, CreatedBy)" +
                " VALUES(\'admin\', \'Admin can manages all resources.\',SYSUTCDATETIME(),0)" +
                " END");
            migrationBuilder.Sql(
                "IF (NOT EXISTS(SELECT * FROM [Role] WHERE Name = \'manager\'))" +
                " BEGIN" +
                " INSERT INTO [Role](Name, Description, CreatedDate, CreatedBy)" +
                " VALUES(\'manager\', \'Manager consults users.\',SYSUTCDATETIME(),0)" +
                " END");
            migrationBuilder.Sql(
                "IF (NOT EXISTS(SELECT * FROM [Role] WHERE Name = \'system\'))" +
                " BEGIN" +
                " INSERT INTO [Role](Name, Description, CreatedDate, CreatedBy)" +
                " VALUES(\'system\', \'System can have only one system user.\',SYSUTCDATETIME(),0)" +
                " END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}