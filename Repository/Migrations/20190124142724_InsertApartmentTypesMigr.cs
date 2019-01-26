using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;

namespace Repository.Migrations
{
    public partial class InsertApartmentTypesMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*var types = _configuration.GetSection("ApartmentTypes").Get<List<string>>();*/
            string[] types =
            {
                "Apartment", "Barn", "Boat", "Bungalow", "Cabin", "CampGround", "Castle", "Chalet", "Condo", "Cottage",
                "Estate",
                "Farm House", "Guest House", "Hostel", "Hotel", "House", "HouseBoat", "Lodge", "Mill", "Mobile Home",
                "Studio",
                "Villa", "Yacht"
            };
            foreach (var type in  types)
            {
                migrationBuilder.Sql(
                    $"IF (NOT EXISTS(SELECT * FROM [ApartmentType] WHERE Name = \'{type}\'))" +                
                    "INSERT INTO [ApartmentType](Name, Description, CreatedDate,CreatedBy)" +
                    $"VALUES(\'{type}\',\'desc\',SYSUTCDATETIME(), 0)");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}