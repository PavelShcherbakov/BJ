using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BJ.DAL.Migrations
{
    public partial class AddGameIdAndCardIdToSteps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BotsSteps_Bots_BotId",
                table: "BotsSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_BotsSteps_Games_GameId",
                table: "BotsSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersSteps_Games_GameId",
                table: "UsersSteps");

            migrationBuilder.DropIndex(
                name: "IX_UsersSteps_GameId",
                table: "UsersSteps");

            migrationBuilder.DropIndex(
                name: "IX_BotsSteps_GameId",
                table: "BotsSteps");

            migrationBuilder.AddColumn<Guid>(
                name: "CardId",
                table: "UsersSteps",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "BotId",
                table: "BotsSteps",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CardId",
                table: "BotsSteps",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_BotsSteps_Bots_BotId",
                table: "BotsSteps",
                column: "BotId",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BotsSteps_Bots_BotId",
                table: "BotsSteps");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "UsersSteps");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "BotsSteps");

            migrationBuilder.AlterColumn<Guid>(
                name: "BotId",
                table: "BotsSteps",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_UsersSteps_GameId",
                table: "UsersSteps",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_BotsSteps_GameId",
                table: "BotsSteps",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_BotsSteps_Bots_BotId",
                table: "BotsSteps",
                column: "BotId",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BotsSteps_Games_GameId",
                table: "BotsSteps",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersSteps_Games_GameId",
                table: "UsersSteps",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
