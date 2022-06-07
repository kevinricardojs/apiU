using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace WebApiSistema.Migrations
{
    public partial class aquiler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alquiler",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SocioNegocioID = table.Column<int>(type: "int", nullable: false),
                    SucursalID = table.Column<int>(type: "int", nullable: false),
                    FechaInicial = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime", nullable: false),
                    Observaciones = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alquiler", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Alquiler_SocioNegocio_SocioNegocioID",
                        column: x => x.SocioNegocioID,
                        principalTable: "SocioNegocio",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alquiler_Sucursal_SucursalID",
                        column: x => x.SucursalID,
                        principalTable: "Sucursal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlquilerDetalles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AlquilerID = table.Column<int>(type: "int", nullable: false),
                    ProductoID = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    NoLinea = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlquilerDetalles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AlquilerDetalles_Alquiler_AlquilerID",
                        column: x => x.AlquilerID,
                        principalTable: "Alquiler",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlquilerDetalles_Producto_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "Producto",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alquiler_SocioNegocioID",
                table: "Alquiler",
                column: "SocioNegocioID");

            migrationBuilder.CreateIndex(
                name: "IX_Alquiler_SucursalID",
                table: "Alquiler",
                column: "SucursalID");

            migrationBuilder.CreateIndex(
                name: "IX_AlquilerDetalles_AlquilerID",
                table: "AlquilerDetalles",
                column: "AlquilerID");

            migrationBuilder.CreateIndex(
                name: "IX_AlquilerDetalles_ProductoID",
                table: "AlquilerDetalles",
                column: "ProductoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlquilerDetalles");

            migrationBuilder.DropTable(
                name: "Alquiler");
        }
    }
}
