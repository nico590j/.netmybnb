using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mybnb.api.Migrations
{
    public partial class MinimumRentingDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinimumRentingDays",
                table: "PossibleRentingPeriods",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinimumRentingDays",
                table: "PossibleRentingPeriods");
        }
    }
}
