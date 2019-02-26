using Microsoft.EntityFrameworkCore.Migrations;

namespace BJ.DAL.Migrations
{
    public partial class AddFildNumOfCardInBotsPoints2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumOfCard",
                table: "BotsPoints",
                newName: "CardsInHand");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CardsInHand",
                table: "BotsPoints",
                newName: "NumOfCard");
        }
    }
}
