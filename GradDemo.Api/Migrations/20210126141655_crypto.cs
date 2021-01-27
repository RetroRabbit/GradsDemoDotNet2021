using Microsoft.EntityFrameworkCore.Migrations;

namespace GradDemo.Api.Migrations
{
    public partial class crypto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShirtSize",
                table: "AspNetUsers",
                newName: "currency");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "currency",
                table: "AspNetUsers",
                newName: "ShirtSize");
        }
    }
}
