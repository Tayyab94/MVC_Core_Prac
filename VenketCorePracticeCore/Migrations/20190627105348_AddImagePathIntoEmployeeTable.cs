using Microsoft.EntityFrameworkCore.Migrations;

namespace VenketCorePracticeCore.Migrations
{
    public partial class AddImagePathIntoEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imagePath",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagePath",
                table: "Employees");
        }
    }
}
