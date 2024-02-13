using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airline.DAL.Migrations
{
    public partial class addTicketFlight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tickets_AspNetUsers_userId",
                table: "tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_tickets_flights_FlightId",
                table: "tickets");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "tickets",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FlightId",
                table: "tickets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_AspNetUsers_userId",
                table: "tickets",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_flights_FlightId",
                table: "tickets",
                column: "FlightId",
                principalTable: "flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tickets_AspNetUsers_userId",
                table: "tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_tickets_flights_FlightId",
                table: "tickets");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "tickets",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "FlightId",
                table: "tickets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_AspNetUsers_userId",
                table: "tickets",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_flights_FlightId",
                table: "tickets",
                column: "FlightId",
                principalTable: "flights",
                principalColumn: "Id");
        }
    }
}
