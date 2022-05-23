using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcSuperShop.Migrations
{
    public partial class Icon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Manufacturers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Manufacturers");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Categories");
        }
    }
}
