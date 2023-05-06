using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tuto.Migrations
{
    public partial class facture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facture_Reservation_IdReservation",
                table: "Facture");

            migrationBuilder.RenameColumn(
                name: "IdReservation",
                table: "Facture",
                newName: "IdClient");

            migrationBuilder.RenameIndex(
                name: "IX_Facture_IdReservation",
                table: "Facture",
                newName: "IX_Facture_IdClient");

            migrationBuilder.AddForeignKey(
                name: "FK_Facture_Client_IdClient",
                table: "Facture",
                column: "IdClient",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facture_Client_IdClient",
                table: "Facture");

            migrationBuilder.RenameColumn(
                name: "IdClient",
                table: "Facture",
                newName: "IdReservation");

            migrationBuilder.RenameIndex(
                name: "IX_Facture_IdClient",
                table: "Facture",
                newName: "IX_Facture_IdReservation");

            migrationBuilder.AddForeignKey(
                name: "FK_Facture_Reservation_IdReservation",
                table: "Facture",
                column: "IdReservation",
                principalTable: "Reservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
