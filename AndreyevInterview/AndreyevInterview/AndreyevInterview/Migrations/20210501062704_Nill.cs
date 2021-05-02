using Microsoft.EntityFrameworkCore.Migrations;

namespace AndreyevInterview.Migrations
{
    public partial class Nill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Bill",
                table: "LineItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bill",
                table: "LineItems");
        }
    }
}
