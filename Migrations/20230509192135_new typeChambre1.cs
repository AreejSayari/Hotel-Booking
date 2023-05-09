using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tuto.Migrations
{
    public partial class newtypeChambre1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chambre_TypeChambre_TypeChambreId",
                table: "Chambre");

            migrationBuilder.DropTable(
                name: "TypeChambre");

            migrationBuilder.DropIndex(
                name: "IX_Chambre_TypeChambreId",
                table: "Chambre");

            migrationBuilder.DropColumn(
                name: "TypeChambreId",
                table: "Chambre");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeChambreId",
                table: "Chambre",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TypeChambre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeChambre", x => x.Id);
                });

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
    }
}
