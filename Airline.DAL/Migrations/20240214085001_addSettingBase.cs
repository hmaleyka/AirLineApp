using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airline.DAL.Migrations
{
    public partial class addSettingBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "setting",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "setting");
        }
    }
}
