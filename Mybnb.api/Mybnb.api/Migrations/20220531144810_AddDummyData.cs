using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mybnb.api.Migrations
{
    public partial class AddDummyData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PossibleRentingPeriods_BNBs_BNBID",
                table: "PossibleRentingPeriods");

            migrationBuilder.AlterColumn<int>(
                name: "BNBID",
                table: "PossibleRentingPeriods",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "FullName", "Password" },
                values: new object[] { -1, "test@edu.ucl.dk", "test", "9F86D081884C7D659A2FEAA0C55AD015A3BF4F1B2B0B822CD15D6C15B0F00A08" });

            migrationBuilder.InsertData(
                table: "BNBs",
                columns: new[] { "ID", "Address", "Country", "Description", "OwnerUserID", "Title", "TypeOfEstablishment", "ZipCode" },
                values: new object[] { -1, "test", "DK", "test data beskrivelse", -1, "test", 0, "1234" });

            migrationBuilder.InsertData(
                table: "PossibleRentingPeriods",
                columns: new[] { "PossibleRentingPeriodID", "BNBID", "DailyPrice", "EndDate", "MinimumRentingDays", "StartDate" },
                values: new object[] { -1, -1, 123456.0, new DateTime(2022, 6, 24, 16, 48, 10, 468, DateTimeKind.Local).AddTicks(7021), 12, new DateTime(2022, 5, 31, 16, 48, 10, 468, DateTimeKind.Local).AddTicks(7000) });

            migrationBuilder.AddForeignKey(
                name: "FK_PossibleRentingPeriods_BNBs_BNBID",
                table: "PossibleRentingPeriods",
                column: "BNBID",
                principalTable: "BNBs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PossibleRentingPeriods_BNBs_BNBID",
                table: "PossibleRentingPeriods");

            migrationBuilder.DeleteData(
                table: "PossibleRentingPeriods",
                keyColumn: "PossibleRentingPeriodID",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "BNBs",
                keyColumn: "ID",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "BNBID",
                table: "PossibleRentingPeriods",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PossibleRentingPeriods_BNBs_BNBID",
                table: "PossibleRentingPeriods",
                column: "BNBID",
                principalTable: "BNBs",
                principalColumn: "ID");
        }
    }
}
