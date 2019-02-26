using Microsoft.EntityFrameworkCore.Migrations;

namespace BJ.DAL.Migrations
{
    public partial class AddFildIsActiveUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IsActive",
                table: "Games",
                nullable: false,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Games",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
