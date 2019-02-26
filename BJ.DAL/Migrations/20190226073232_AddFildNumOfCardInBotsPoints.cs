using Microsoft.EntityFrameworkCore.Migrations;

namespace BJ.DAL.Migrations
{
    public partial class AddFildNumOfCardInBotsPoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumOfCard",
                table: "BotsPoints",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumOfCard",
                table: "BotsPoints");
        }
    }
}
