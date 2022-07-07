using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CivilizationAPI.Migrations
{
    public partial class DefaultsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Missions",
                newName: "RealDescription");

            migrationBuilder.RenameColumn(
                name: "Condition",
                table: "Missions",
                newName: "MetaDescription");

            migrationBuilder.InsertData(
                table: "MissionTypes",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Стандарт" },
                    { 2, "Дуэль" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MissionTypes",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MissionTypes",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "RealDescription",
                table: "Missions",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "MetaDescription",
                table: "Missions",
                newName: "Condition");
        }
    }
}
