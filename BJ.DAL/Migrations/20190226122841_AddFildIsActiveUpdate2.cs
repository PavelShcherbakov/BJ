using Microsoft.EntityFrameworkCore.Migrations;

namespace BJ.DAL.Migrations
{
    public partial class AddFildIsActiveUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Games",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "IsActive",
                table: "Games",
                nullable: false,
                defaultValue: 0);
        }
    }
}
