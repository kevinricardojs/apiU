using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiSistema.Migrations
{
    public partial class AgregarCodigoCuenta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FamiliaProductoID",
                table: "Producto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CodigoCuenta",
                table: "Cuenta",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Producto_FamiliaProductoID",
                table: "Producto",
                column: "FamiliaProductoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_FamiliaProducto_FamiliaProductoID",
                table: "Producto",
                column: "FamiliaProductoID",
                principalTable: "FamiliaProducto",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_FamiliaProducto_FamiliaProductoID",
                table: "Producto");

            migrationBuilder.DropIndex(
                name: "IX_Producto_FamiliaProductoID",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "FamiliaProductoID",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "CodigoCuenta",
                table: "Cuenta");
        }
    }
}
