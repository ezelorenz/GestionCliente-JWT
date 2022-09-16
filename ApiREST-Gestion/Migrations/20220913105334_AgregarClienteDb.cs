using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiREST_Gestion.Migrations
{
    public partial class AgregarClienteDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
