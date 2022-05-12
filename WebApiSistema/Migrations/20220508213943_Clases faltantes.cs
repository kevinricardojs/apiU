using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace WebApiSistema.Migrations
{
    public partial class Clasesfaltantes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cuenta",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuenta", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductoTipo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    SucursalID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoTipo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductoTipo_Sucursal_SucursalID",
                        column: x => x.SucursalID,
                        principalTable: "Sucursal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SocioNegocio",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Telefono = table.Column<string>(type: "text", nullable: true),
                    Direccion = table.Column<string>(type: "text", nullable: true),
                    Nit = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocioNegocio", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TransaccionContable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FechaHora = table.Column<DateTime>(type: "datetime", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    CompraVentaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransaccionContable", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TransaccionInventario",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FechaHora = table.Column<DateTime>(type: "datetime", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    CompraVentaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransaccionInventario", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CuentaPresupuesto",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CuentaID = table.Column<int>(type: "int", nullable: false),
                    SucursalID = table.Column<int>(type: "int", nullable: false),
                    Presupuesto = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Mes = table.Column<int>(type: "int", nullable: false),
                    Anio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentaPresupuesto", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CuentaPresupuesto_Cuenta_CuentaID",
                        column: x => x.CuentaID,
                        principalTable: "Cuenta",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CuentaPresupuesto_Sucursal_SucursalID",
                        column: x => x.SucursalID,
                        principalTable: "Sucursal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FamiliaProducto",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    CuentaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamiliaProducto", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FamiliaProducto_Cuenta_CuentaID",
                        column: x => x.CuentaID,
                        principalTable: "Cuenta",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ProductoTipoID = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    SucursalID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Producto_ProductoTipo_ProductoTipoID",
                        column: x => x.ProductoTipoID,
                        principalTable: "ProductoTipo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Producto_Sucursal_SucursalID",
                        column: x => x.SucursalID,
                        principalTable: "Sucursal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SocioNegocioID = table.Column<int>(type: "int", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime", nullable: false),
                    FacturaSerie = table.Column<string>(type: "text", nullable: true),
                    FacturaFecha = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Compra_SocioNegocio_SocioNegocioID",
                        column: x => x.SocioNegocioID,
                        principalTable: "SocioNegocio",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransaccionDetalleContable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CuentaID = table.Column<int>(type: "int", nullable: false),
                    SucursalID = table.Column<int>(type: "int", nullable: false),
                    Linea = table.Column<int>(type: "int", nullable: false),
                    Debe = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Haber = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime", nullable: false),
                    TransaccionContableID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransaccionDetalleContable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransaccionDetalleContable_Cuenta_CuentaID",
                        column: x => x.CuentaID,
                        principalTable: "Cuenta",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransaccionDetalleContable_Sucursal_SucursalID",
                        column: x => x.SucursalID,
                        principalTable: "Sucursal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransaccionDetalleContable_TransaccionContable_TransaccionCo~",
                        column: x => x.TransaccionContableID,
                        principalTable: "TransaccionContable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransaccionDetalleInventario",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ProductoID = table.Column<int>(type: "int", nullable: false),
                    SucursalID = table.Column<int>(type: "int", nullable: false),
                    Linea = table.Column<int>(type: "int", nullable: false),
                    Entrada = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Salida = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime", nullable: false),
                    TransaccionInventarioID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransaccionDetalleInventario", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransaccionDetalleInventario_Producto_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "Producto",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransaccionDetalleInventario_Sucursal_SucursalID",
                        column: x => x.SucursalID,
                        principalTable: "Sucursal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransaccionDetalleInventario_TransaccionInventario_Transacci~",
                        column: x => x.TransaccionInventarioID,
                        principalTable: "TransaccionInventario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompraDetalle",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CompraID = table.Column<int>(type: "int", nullable: false),
                    ProductoID = table.Column<int>(type: "int", nullable: false),
                    NoLinea = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraDetalle", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CompraDetalle_Compra_CompraID",
                        column: x => x.CompraID,
                        principalTable: "Compra",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompraDetalle_Producto_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "Producto",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compra_SocioNegocioID",
                table: "Compra",
                column: "SocioNegocioID");

            migrationBuilder.CreateIndex(
                name: "IX_CompraDetalle_CompraID",
                table: "CompraDetalle",
                column: "CompraID");

            migrationBuilder.CreateIndex(
                name: "IX_CompraDetalle_ProductoID",
                table: "CompraDetalle",
                column: "ProductoID");

            migrationBuilder.CreateIndex(
                name: "IX_CuentaPresupuesto_CuentaID",
                table: "CuentaPresupuesto",
                column: "CuentaID");

            migrationBuilder.CreateIndex(
                name: "IX_CuentaPresupuesto_SucursalID",
                table: "CuentaPresupuesto",
                column: "SucursalID");

            migrationBuilder.CreateIndex(
                name: "IX_FamiliaProducto_CuentaID",
                table: "FamiliaProducto",
                column: "CuentaID");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_ProductoTipoID",
                table: "Producto",
                column: "ProductoTipoID");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_SucursalID",
                table: "Producto",
                column: "SucursalID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoTipo_SucursalID",
                table: "ProductoTipo",
                column: "SucursalID");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionDetalleContable_CuentaID",
                table: "TransaccionDetalleContable",
                column: "CuentaID");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionDetalleContable_SucursalID",
                table: "TransaccionDetalleContable",
                column: "SucursalID");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionDetalleContable_TransaccionContableID",
                table: "TransaccionDetalleContable",
                column: "TransaccionContableID");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionDetalleInventario_ProductoID",
                table: "TransaccionDetalleInventario",
                column: "ProductoID");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionDetalleInventario_SucursalID",
                table: "TransaccionDetalleInventario",
                column: "SucursalID");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionDetalleInventario_TransaccionInventarioID",
                table: "TransaccionDetalleInventario",
                column: "TransaccionInventarioID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompraDetalle");

            migrationBuilder.DropTable(
                name: "CuentaPresupuesto");

            migrationBuilder.DropTable(
                name: "FamiliaProducto");

            migrationBuilder.DropTable(
                name: "TransaccionDetalleContable");

            migrationBuilder.DropTable(
                name: "TransaccionDetalleInventario");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "Cuenta");

            migrationBuilder.DropTable(
                name: "TransaccionContable");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "TransaccionInventario");

            migrationBuilder.DropTable(
                name: "SocioNegocio");

            migrationBuilder.DropTable(
                name: "ProductoTipo");
        }
    }
}
