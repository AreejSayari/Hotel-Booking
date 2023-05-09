using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tuto.Migrations
{
    public partial class newtypeChambre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chambre_TypeChambre_IdTypeChambre",
                table: "Chambre");

            migrationBuilder.DropIndex(
                name: "IX_Chambre_IdTypeChambre",
                table: "Chambre");

            migrationBuilder.DropColumn(
                name: "IdTypeChambre",
                table: "Chambre");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TypeChambre",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "TypeChambreId",
                table: "Chambre",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chambre_TypeChambreId",
                table: "Chambre",
                column: "TypeChambreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chambre_TypeChambre_TypeChambreId",
                table: "Chambre",
                column: "TypeChambreId",
                principalTable: "TypeChambre",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chambre_TypeChambre_TypeChambreId",
                table: "Chambre");

            migrationBuilder.DropIndex(
                name: "IX_Chambre_TypeChambreId",
                table: "Chambre");

            migrationBuilder.DropColumn(
                name: "TypeChambreId",
                table: "Chambre");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TypeChambre",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdTypeChambre",
                table: "Chambre",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Chambre_IdTypeChambre",
                table: "Chambre",
                column: "IdTypeChambre");

            migrationBuilder.AddForeignKey(
                name: "FK_Chambre_TypeChambre_IdTypeChambre",
                table: "Chambre",
                column: "IdTypeChambre",
                principalTable: "TypeChambre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
