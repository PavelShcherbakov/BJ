using Microsoft.EntityFrameworkCore.Migrations;

namespace BJ.DAL.Migrations
{
    public partial class AddBotsPointsTable3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BotsPoints_BotId",
                table: "BotsPoints",
                column: "BotId");

            migrationBuilder.AddForeignKey(
                name: "FK_BotsPoints_Bots_BotId",
                table: "BotsPoints",
                column: "BotId",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BotsPoints_Bots_BotId",
                table: "BotsPoints");

            migrationBuilder.DropIndex(
                name: "IX_BotsPoints_BotId",
                table: "BotsPoints");
        }
    }
}
