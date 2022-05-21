using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mybnb.api.Migrations
{
    public partial class RefactoredBNB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BNBs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OwnerUserID",
                table: "BNBs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "BNBs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BNBs_OwnerUserID",
                table: "BNBs",
                column: "OwnerUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_BNBs_Users_OwnerUserID",
                table: "BNBs",
                column: "OwnerUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BNBs_Users_OwnerUserID",
                table: "BNBs");

            migrationBuilder.DropIndex(
                name: "IX_BNBs_OwnerUserID",
                table: "BNBs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "BNBs");

            migrationBuilder.DropColumn(
                name: "OwnerUserID",
                table: "BNBs");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "BNBs");
        }
    }
}
