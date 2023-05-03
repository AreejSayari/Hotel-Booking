using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tuto.Migrations
{
    public partial class quatreee1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Client_ClientId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_ClientId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Reservation");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_IdClient",
                table: "Reservation",
                column: "IdClient");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Client_IdClient",
                table: "Reservation",
                column: "IdClient",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Client_IdClient",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_IdClient",
                table: "Reservation");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ClientId",
                table: "Reservation",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Client_ClientId",
                table: "Reservation",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id");
        }
    }
}
