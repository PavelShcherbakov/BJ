using Microsoft.EntityFrameworkCore.Migrations;

namespace BJ.DAL.Migrations
{
    public partial class AddModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bot_Games_GameId1",
                table: "Bot");

            migrationBuilder.DropForeignKey(
                name: "FK_BotsStep_Bot_BotId",
                table: "BotsStep");

            migrationBuilder.DropForeignKey(
                name: "FK_BotsStep_Games_GameId",
                table: "BotsStep");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_Games_GameId1",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersStep_Games_GameId",
                table: "UsersStep");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersStep_AspNetUsers_UserId",
                table: "UsersStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersStep",
                table: "UsersStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Card",
                table: "Card");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BotsStep",
                table: "BotsStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bot",
                table: "Bot");

            migrationBuilder.RenameTable(
                name: "UsersStep",
                newName: "UsersSteps");

            migrationBuilder.RenameTable(
                name: "Card",
                newName: "Cards");

            migrationBuilder.RenameTable(
                name: "BotsStep",
                newName: "BotsSteps");

            migrationBuilder.RenameTable(
                name: "Bot",
                newName: "Bots");

            migrationBuilder.RenameIndex(
                name: "IX_UsersStep_UserId",
                table: "UsersSteps",
                newName: "IX_UsersSteps_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersStep_GameId",
                table: "UsersSteps",
                newName: "IX_UsersSteps_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Card_GameId1",
                table: "Cards",
                newName: "IX_Cards_GameId1");

            migrationBuilder.RenameIndex(
                name: "IX_BotsStep_GameId",
                table: "BotsSteps",
                newName: "IX_BotsSteps_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_BotsStep_BotId",
                table: "BotsSteps",
                newName: "IX_BotsSteps_BotId");

            migrationBuilder.RenameIndex(
                name: "IX_Bot_GameId1",
                table: "Bots",
                newName: "IX_Bots_GameId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersSteps",
                table: "UsersSteps",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cards",
                table: "Cards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BotsSteps",
                table: "BotsSteps",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bots",
                table: "Bots",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bots_Games_GameId1",
                table: "Bots",
                column: "GameId1",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Cards_Games_GameId1",
                table: "Cards",
                column: "GameId1",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersSteps_Games_GameId",
                table: "UsersSteps",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersSteps_AspNetUsers_UserId",
                table: "UsersSteps",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bots_Games_GameId1",
                table: "Bots");

            migrationBuilder.DropForeignKey(
                name: "FK_BotsSteps_Bots_BotId",
                table: "BotsSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_BotsSteps_Games_GameId",
                table: "BotsSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Games_GameId1",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersSteps_Games_GameId",
                table: "UsersSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersSteps_AspNetUsers_UserId",
                table: "UsersSteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersSteps",
                table: "UsersSteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cards",
                table: "Cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BotsSteps",
                table: "BotsSteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bots",
                table: "Bots");

            migrationBuilder.RenameTable(
                name: "UsersSteps",
                newName: "UsersStep");

            migrationBuilder.RenameTable(
                name: "Cards",
                newName: "Card");

            migrationBuilder.RenameTable(
                name: "BotsSteps",
                newName: "BotsStep");

            migrationBuilder.RenameTable(
                name: "Bots",
                newName: "Bot");

            migrationBuilder.RenameIndex(
                name: "IX_UsersSteps_UserId",
                table: "UsersStep",
                newName: "IX_UsersStep_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersSteps_GameId",
                table: "UsersStep",
                newName: "IX_UsersStep_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_GameId1",
                table: "Card",
                newName: "IX_Card_GameId1");

            migrationBuilder.RenameIndex(
                name: "IX_BotsSteps_GameId",
                table: "BotsStep",
                newName: "IX_BotsStep_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_BotsSteps_BotId",
                table: "BotsStep",
                newName: "IX_BotsStep_BotId");

            migrationBuilder.RenameIndex(
                name: "IX_Bots_GameId1",
                table: "Bot",
                newName: "IX_Bot_GameId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersStep",
                table: "UsersStep",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Card",
                table: "Card",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BotsStep",
                table: "BotsStep",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bot",
                table: "Bot",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bot_Games_GameId1",
                table: "Bot",
                column: "GameId1",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BotsStep_Bot_BotId",
                table: "BotsStep",
                column: "BotId",
                principalTable: "Bot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BotsStep_Games_GameId",
                table: "BotsStep",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Games_GameId1",
                table: "Card",
                column: "GameId1",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersStep_Games_GameId",
                table: "UsersStep",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersStep_AspNetUsers_UserId",
                table: "UsersStep",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
