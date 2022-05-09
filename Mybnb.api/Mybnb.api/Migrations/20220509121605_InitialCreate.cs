using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mybnb.api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BNBs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfEstablishment = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BNBs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    BNBID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Images_BNBs_BNBID",
                        column: x => x.BNBID,
                        principalTable: "BNBs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PossibleRentingPeriods",
                columns: table => new
                {
                    PossibleRentingPeriodID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DailyPrice = table.Column<double>(type: "float", nullable: false),
                    BNBID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PossibleRentingPeriods", x => x.PossibleRentingPeriodID);
                    table.ForeignKey(
                        name: "FK_PossibleRentingPeriods_BNBs_BNBID",
                        column: x => x.BNBID,
                        principalTable: "BNBs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TenantPeriods",
                columns: table => new
                {
                    TenantPeriodID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantUserID = table.Column<int>(type: "int", nullable: false),
                    BNBID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantPeriods", x => x.TenantPeriodID);
                    table.ForeignKey(
                        name: "FK_TenantPeriods_BNBs_BNBID",
                        column: x => x.BNBID,
                        principalTable: "BNBs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TenantPeriods_Users_TenantUserID",
                        column: x => x.TenantUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_BNBID",
                table: "Images",
                column: "BNBID");

            migrationBuilder.CreateIndex(
                name: "IX_PossibleRentingPeriods_BNBID",
                table: "PossibleRentingPeriods",
                column: "BNBID");

            migrationBuilder.CreateIndex(
                name: "IX_TenantPeriods_BNBID",
                table: "TenantPeriods",
                column: "BNBID");

            migrationBuilder.CreateIndex(
                name: "IX_TenantPeriods_TenantUserID",
                table: "TenantPeriods",
                column: "TenantUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "PossibleRentingPeriods");

            migrationBuilder.DropTable(
                name: "TenantPeriods");

            migrationBuilder.DropTable(
                name: "BNBs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
