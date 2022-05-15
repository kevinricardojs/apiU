using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace WebApiSistema.Migrations
{
    public partial class Aniadirventas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "TransaccionDetalleInventario",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "CuentaIDO",
                table: "FamiliaProducto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Cantidad",
                table: "CompraDetalle",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Venta",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SocioNegocioID = table.Column<int>(type: "int", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime", nullable: false),
                    FacturaSerie = table.Column<string>(type: "text", nullable: true),
                    FacturaFecha = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venta", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Venta_SocioNegocio_SocioNegocioID",
                        column: x => x.SocioNegocioID,
                        principalTable: "SocioNegocio",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VentaDetalle",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    VentaID = table.Column<int>(type: "int", nullable: false),
                    ProductoID = table.Column<int>(type: "int", nullable: false),
                    NoLinea = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentaDetalle", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VentaDetalle_Producto_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "Producto",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VentaDetalle_Venta_VentaID",
                        column: x => x.VentaID,
                        principalTable: "Venta",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Venta_SocioNegocioID",
                table: "Venta",
                column: "SocioNegocioID");

            migrationBuilder.CreateIndex(
                name: "IX_VentaDetalle_ProductoID",
                table: "VentaDetalle",
                column: "ProductoID");

            migrationBuilder.CreateIndex(
                name: "IX_VentaDetalle_VentaID",
                table: "VentaDetalle",
                column: "VentaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VentaDetalle");

            migrationBuilder.DropTable(
                name: "Venta");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "TransaccionDetalleInventario");

            migrationBuilder.DropColumn(
                name: "CuentaIDO",
                table: "FamiliaProducto");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "CompraDetalle");
        }
    }
}
