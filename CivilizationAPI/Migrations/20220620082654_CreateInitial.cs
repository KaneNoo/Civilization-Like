using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CivilizationAPI.Migrations
{
    public partial class CreateInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Prizes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prizes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PlayersPrizes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    PrizeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayersPrizes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlayersPrizes_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayersPrizes_Prizes_PrizeID",
                        column: x => x.PrizeID,
                        principalTable: "Prizes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wheels",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    PrizeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wheels", x => new { x.ID, x.PrizeID });
                    table.ForeignKey(
                        name: "FK_Wheels_Prizes_PrizeID",
                        column: x => x.PrizeID,
                        principalTable: "Prizes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayersPrizes_PlayerID",
                table: "PlayersPrizes",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayersPrizes_PrizeID",
                table: "PlayersPrizes",
                column: "PrizeID");

            migrationBuilder.CreateIndex(
                name: "IX_Wheels_PrizeID",
                table: "Wheels",
                column: "PrizeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayersPrizes");

            migrationBuilder.DropTable(
                name: "Wheels");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Prizes");
        }
    }
}
