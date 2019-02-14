using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BJ.DAL.Migrations
{
    public partial class AddBotsPointsTable4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bots_Games_GameId",
                table: "Bots");

            migrationBuilder.DropIndex(
                name: "IX_Bots_GameId",
                table: "Bots");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Bots");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "Bots");

            migrationBuilder.CreateIndex(
                name: "IX_BotsSteps_GameId",
                table: "BotsSteps",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_BotsPoints_GameId",
                table: "BotsPoints",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_BotsPoints_Games_GameId",
                table: "BotsPoints",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BotsSteps_Games_GameId",
                table: "BotsSteps",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BotsPoints_Games_GameId",
                table: "BotsPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_BotsSteps_Games_GameId",
                table: "BotsSteps");

            migrationBuilder.DropIndex(
                name: "IX_BotsSteps_GameId",
                table: "BotsSteps");

            migrationBuilder.DropIndex(
                name: "IX_BotsPoints_GameId",
                table: "BotsPoints");

            migrationBuilder.AddColumn<Guid>(
                name: "GameId",
                table: "Bots",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Bots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bots_GameId",
                table: "Bots",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bots_Games_GameId",
                table: "Bots",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
