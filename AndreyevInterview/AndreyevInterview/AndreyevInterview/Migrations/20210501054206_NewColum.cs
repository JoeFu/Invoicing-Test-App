using Microsoft.EntityFrameworkCore.Migrations;

namespace AndreyevInterview.Migrations
{
    public partial class NewColum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalBillableValue",
                table: "Invoices",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalNumberLineItems",
                table: "Invoices",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalValue",
                table: "Invoices",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalBillableValue",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TotalNumberLineItems",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TotalValue",
                table: "Invoices");
        }
    }
}
