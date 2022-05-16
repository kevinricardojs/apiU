using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace WebApiSistema.Migrations
{
    public partial class Listademateriales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VentaDetalle_Producto_ProductoID",
                table: "VentaDetalle");

            migrationBuilder.DropForeignKey(
                name: "FK_VentaDetalle_Venta_VentaID",
                table: "VentaDetalle");

            migrationBuilder.DropIndex(
                name: "IX_VentaDetalle_ProductoID",
                table: "VentaDetalle");

            migrationBuilder.AlterColumn<int>(
                name: "VentaID",
                table: "VentaDetalle",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ListaMateriales",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ProductoID = table.Column<int>(type: "int", nullable: false),
                    Creado = table.Column<DateTime>(type: "datetime", nullable: false),
                    Instrucciones = table.Column<string>(type: "text", nullable: true),
                    Cantidad = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaMateriales", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ListaMateriales_Producto_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "Producto",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materiales",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ListaMaterialesID = table.Column<int>(type: "int", nullable: false),
                    NoLinea = table.Column<int>(type: "int", nullable: false),
                    ProductoID = table.Column<int>(type: "int", nullable: false),
                    Instrucciones = table.Column<string>(type: "text", nullable: true),
                    Cantidad = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materiales", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Materiales_ListaMateriales_ListaMaterialesID",
                        column: x => x.ListaMaterialesID,
                        principalTable: "ListaMateriales",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Materiales_Producto_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "Producto",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListaMateriales_ProductoID",
                table: "ListaMateriales",
                column: "ProductoID");

            migrationBuilder.CreateIndex(
                name: "IX_Materiales_ListaMaterialesID",
                table: "Materiales",
                column: "ListaMaterialesID");

            migrationBuilder.CreateIndex(
                name: "IX_Materiales_ProductoID",
                table: "Materiales",
                column: "ProductoID");

            migrationBuilder.AddForeignKey(
                name: "FK_VentaDetalle_Venta_VentaID",
                table: "VentaDetalle",
                column: "VentaID",
                principalTable: "Venta",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VentaDetalle_Venta_VentaID",
                table: "VentaDetalle");

            migrationBuilder.DropTable(
                name: "Materiales");

            migrationBuilder.DropTable(
                name: "ListaMateriales");

            migrationBuilder.AlterColumn<int>(
                name: "VentaID",
                table: "VentaDetalle",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VentaDetalle_ProductoID",
                table: "VentaDetalle",
                column: "ProductoID");

            migrationBuilder.AddForeignKey(
                name: "FK_VentaDetalle_Producto_ProductoID",
                table: "VentaDetalle",
                column: "ProductoID",
                principalTable: "Producto",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VentaDetalle_Venta_VentaID",
                table: "VentaDetalle",
                column: "VentaID",
                principalTable: "Venta",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
