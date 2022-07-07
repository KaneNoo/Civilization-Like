using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CivilizationAPI.Migrations
{
    public partial class DistrictNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Districts_DistrictID",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_DistrictID",
                table: "Players");

            migrationBuilder.AlterColumn<int>(
                name: "DistrictID",
                table: "Players",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Players_DistrictID",
                table: "Players",
                column: "DistrictID",
                unique: true,
                filter: "[DistrictID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Districts_DistrictID",
                table: "Players",
                column: "DistrictID",
                principalTable: "Districts",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Districts_DistrictID",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_DistrictID",
                table: "Players");

            migrationBuilder.AlterColumn<int>(
                name: "DistrictID",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_DistrictID",
                table: "Players",
                column: "DistrictID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Districts_DistrictID",
                table: "Players",
                column: "DistrictID",
                principalTable: "Districts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
