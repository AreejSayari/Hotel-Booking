using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tuto.Migrations
{
    public partial class quatreee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Chambre_ChambreId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_ChambreId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "ChambreId",
                table: "Reservation");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_IdChambre",
                table: "Reservation",
                column: "IdChambre");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Chambre_IdChambre",
                table: "Reservation",
                column: "IdChambre",
                principalTable: "Chambre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Chambre_IdChambre",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_IdChambre",
                table: "Reservation");

            migrationBuilder.AddColumn<int>(
                name: "ChambreId",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ChambreId",
                table: "Reservation",
                column: "ChambreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Chambre_ChambreId",
                table: "Reservation",
                column: "ChambreId",
                principalTable: "Chambre",
                principalColumn: "Id");
        }
    }
}
