using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace WebApiSistema.Migrations
{
    public partial class Servicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Servicio",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SocioNegocioID = table.Column<int>(type: "int", nullable: false),
                    SucursalID = table.Column<int>(type: "int", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime", nullable: false),
                    Observaciones = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicio", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Servicio_SocioNegocio_SocioNegocioID",
                        column: x => x.SocioNegocioID,
                        principalTable: "SocioNegocio",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servicio_Sucursal_SucursalID",
                        column: x => x.SucursalID,
                        principalTable: "Sucursal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicioDetalles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ServicioID = table.Column<int>(type: "int", nullable: false),
                    ProductoID = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    NoLinea = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicioDetalles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ServicioDetalles_Producto_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "Producto",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicioDetalles_Servicio_ServicioID",
                        column: x => x.ServicioID,
                        principalTable: "Servicio",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_SocioNegocioID",
                table: "Servicio",
                column: "SocioNegocioID");

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_SucursalID",
                table: "Servicio",
                column: "SucursalID");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioDetalles_ProductoID",
                table: "ServicioDetalles",
                column: "ProductoID");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioDetalles_ServicioID",
                table: "ServicioDetalles",
                column: "ServicioID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServicioDetalles");

            migrationBuilder.DropTable(
                name: "Servicio");
        }
    }
}
