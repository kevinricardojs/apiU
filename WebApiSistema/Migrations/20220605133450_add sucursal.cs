using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiSistema.Migrations
{
    public partial class addsucursal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SucursalID",
                table: "Venta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SucursalID",
                table: "Compra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Venta_SucursalID",
                table: "Venta",
                column: "SucursalID");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_SucursalID",
                table: "Compra",
                column: "SucursalID");

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Sucursal_SucursalID",
                table: "Compra",
                column: "SucursalID",
                principalTable: "Sucursal",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Venta_Sucursal_SucursalID",
                table: "Venta",
                column: "SucursalID",
                principalTable: "Sucursal",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Sucursal_SucursalID",
                table: "Compra");

            migrationBuilder.DropForeignKey(
                name: "FK_Venta_Sucursal_SucursalID",
                table: "Venta");

            migrationBuilder.DropIndex(
                name: "IX_Venta_SucursalID",
                table: "Venta");

            migrationBuilder.DropIndex(
                name: "IX_Compra_SucursalID",
                table: "Compra");

            migrationBuilder.DropColumn(
                name: "SucursalID",
                table: "Venta");

            migrationBuilder.DropColumn(
                name: "SucursalID",
                table: "Compra");
        }
    }
}
