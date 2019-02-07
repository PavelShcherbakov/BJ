using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BJ.DAL.Migrations
{
    public partial class ModelRestructuring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bots_Games_GameId1",
                table: "Bots");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Games_GameId1",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_GameId1",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Bots_GameId1",
                table: "Bots");

            migrationBuilder.DropColumn(
                name: "GameId1",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "GameId1",
                table: "Bots");

            migrationBuilder.AlterColumn<Guid>(
                name: "GameId",
                table: "UsersSteps",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "UsersSteps",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "UsersSteps",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Games",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Games",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Cards",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Cards",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "GameId",
                table: "Cards",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Cards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Suit",
                table: "Cards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "GameId",
                table: "BotsSteps",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "BotId",
                table: "BotsSteps",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "BotsSteps",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "BotsSteps",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Bots",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Bots",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "GameId",
                table: "Bots",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Decks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    GameId = table.Column<Guid>(nullable: false),
                    CardId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Decks_Games_CardId",
                        column: x => x.CardId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Decks_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_GameId",
                table: "Cards",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Bots_GameId",
                table: "Bots",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Decks_CardId",
                table: "Decks",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Decks_GameId",
                table: "Decks",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bots_Games_GameId",
                table: "Bots",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Games_GameId",
                table: "Cards",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bots_Games_GameId",
                table: "Bots");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Games_GameId",
                table: "Cards");

            migrationBuilder.DropTable(
                name: "Decks");

            migrationBuilder.DropIndex(
                name: "IX_Cards_GameId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Bots_GameId",
                table: "Bots");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "UsersSteps");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Suit",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "BotsSteps");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Bots");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Bots");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "UsersSteps",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UsersSteps",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Games",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Cards",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "GameId1",
                table: "Cards",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "BotsSteps",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "BotId",
                table: "BotsSteps",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "BotsSteps",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Bots",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "GameId1",
                table: "Bots",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_GameId1",
                table: "Cards",
                column: "GameId1");

            migrationBuilder.CreateIndex(
                name: "IX_Bots_GameId1",
                table: "Bots",
                column: "GameId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Bots_Games_GameId1",
                table: "Bots",
                column: "GameId1",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Games_GameId1",
                table: "Cards",
                column: "GameId1",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
