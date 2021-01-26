using Microsoft.EntityFrameworkCore.Migrations;

namespace GradDemo.Api.Migrations
{
    public partial class Addedadevicetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShirtSize",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShirtSize",
                table: "AspNetUsers");
        }
    }
}
