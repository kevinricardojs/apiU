using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiSistema.Migrations
{
    public partial class addsucursaltoprod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SucursalID",
                table: "Produccion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Produccion_SucursalID",
                table: "Produccion",
                column: "SucursalID");

            migrationBuilder.AddForeignKey(
                name: "FK_Produccion_Sucursal_SucursalID",
                table: "Produccion",
                column: "SucursalID",
                principalTable: "Sucursal",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produccion_Sucursal_SucursalID",
                table: "Produccion");

            migrationBuilder.DropIndex(
                name: "IX_Produccion_SucursalID",
                table: "Produccion");

            migrationBuilder.DropColumn(
                name: "SucursalID",
                table: "Produccion");
        }
    }
}
