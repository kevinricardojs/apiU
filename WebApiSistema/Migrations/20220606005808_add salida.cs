using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace WebApiSistema.Migrations
{
    public partial class addsalida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Salida",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Comentarios = table.Column<string>(type: "text", nullable: true),
                    FechaHora = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salida", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SalidaDetalles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SalidaID = table.Column<int>(type: "int", nullable: false),
                    NoLinea = table.Column<int>(type: "int", nullable: false),
                    ProductoID = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalidaDetalles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SalidaDetalles_Producto_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "Producto",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalidaDetalles_Salida_SalidaID",
                        column: x => x.SalidaID,
                        principalTable: "Salida",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalidaDetalles_ProductoID",
                table: "SalidaDetalles",
                column: "ProductoID");

            migrationBuilder.CreateIndex(
                name: "IX_SalidaDetalles_SalidaID",
                table: "SalidaDetalles",
                column: "SalidaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalidaDetalles");

            migrationBuilder.DropTable(
                name: "Salida");
        }
    }
}
