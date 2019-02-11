using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BJ.DAL.Migrations
{
    public partial class RemoveCards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Decks_Cards_CardId",
                table: "Decks");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Decks_CardId",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Decks");

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "UsersSteps",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Suit",
                table: "UsersSteps",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Decks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Suit",
                table: "Decks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "BotsSteps",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Suit",
                table: "BotsSteps",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rank",
                table: "UsersSteps");

            migrationBuilder.DropColumn(
                name: "Suit",
                table: "UsersSteps");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "Suit",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "BotsSteps");

            migrationBuilder.DropColumn(
                name: "Suit",
                table: "BotsSteps");

            migrationBuilder.AddColumn<Guid>(
                name: "CardId",
                table: "Decks",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Rank = table.Column<int>(nullable: false),
                    Suit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Decks_CardId",
                table: "Decks",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Decks_Cards_CardId",
                table: "Decks",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
