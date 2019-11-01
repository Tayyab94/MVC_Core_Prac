using Microsoft.EntityFrameworkCore.Migrations;

namespace VenketCorePracticeCore.Migrations
{
    public partial class ApplySeedMethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "Departments", "Emial", "Name" },
                values: new object[] { 1, 1, "tayyab@gmail.com", "Tayyab" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 1);
        }
    }
}
