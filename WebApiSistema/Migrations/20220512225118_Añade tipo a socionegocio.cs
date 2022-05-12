using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiSistema.Migrations
{
    public partial class Añadetipoasocionegocio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "SocioNegocio",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "SocioNegocio");
        }
    }
}
