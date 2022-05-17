using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace WebApiSistema.Migrations
{
    public partial class Produccion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produccion",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ListaMaterialesID = table.Column<int>(type: "int", nullable: false),
                    Creado = table.Column<DateTime>(type: "datetime", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produccion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Produccion_ListaMateriales_ListaMaterialesID",
                        column: x => x.ListaMaterialesID,
                        principalTable: "ListaMateriales",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProduccionDetalles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ProduccionID = table.Column<int>(type: "int", nullable: false),
                    NoLinea = table.Column<int>(type: "int", nullable: false),
                    ProductoID = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduccionDetalles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProduccionDetalles_Produccion_ProduccionID",
                        column: x => x.ProduccionID,
                        principalTable: "Produccion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProduccionDetalles_Producto_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "Producto",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produccion_ListaMaterialesID",
                table: "Produccion",
                column: "ListaMaterialesID");

            migrationBuilder.CreateIndex(
                name: "IX_ProduccionDetalles_ProduccionID",
                table: "ProduccionDetalles",
                column: "ProduccionID");

            migrationBuilder.CreateIndex(
                name: "IX_ProduccionDetalles_ProductoID",
                table: "ProduccionDetalles",
                column: "ProductoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProduccionDetalles");

            migrationBuilder.DropTable(
                name: "Produccion");
        }
    }
}
