using Microsoft.EntityFrameworkCore.Migrations;

namespace VenketCorePracticeCore.Migrations
{
    public partial class Add_FirstName_Property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstNameofUser",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstNameofUser",
                table: "AspNetUsers");
        }
    }
}
