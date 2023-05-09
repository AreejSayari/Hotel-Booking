using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tuto.Migrations
{
    public partial class newtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdTypeChambre",
                table: "Chambre",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TypeChambre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeChambre", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chambre_TypeChambre_IdTypeChambre",
                table: "Chambre");

            migrationBuilder.DropTable(
                name: "TypeChambre");

            migrationBuilder.DropIndex(
                name: "IX_Chambre_IdTypeChambre",
                table: "Chambre");

            migrationBuilder.DropColumn(
                name: "IdTypeChambre",
                table: "Chambre");
        }
    }
}
