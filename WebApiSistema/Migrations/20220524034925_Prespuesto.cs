using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace WebApiSistema.Migrations
{
    public partial class Prespuesto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SucursalID",
                table: "TransaccionContable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SucursalID",
                table: "SocioNegocio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SucursalID",
                table: "ListaMateriales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PresupuestoID",
                table: "CuentaPresupuesto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Presupuesto",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SucursalID = table.Column<int>(type: "int", nullable: false),
                    Creado = table.Column<DateTime>(type: "datetime", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presupuesto", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Presupuesto_Sucursal_SucursalID",
                        column: x => x.SucursalID,
                        principalTable: "Sucursal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionContable_SucursalID",
                table: "TransaccionContable",
                column: "SucursalID");

            migrationBuilder.CreateIndex(
                name: "IX_SocioNegocio_SucursalID",
                table: "SocioNegocio",
                column: "SucursalID");

            migrationBuilder.CreateIndex(
                name: "IX_ListaMateriales_SucursalID",
                table: "ListaMateriales",
                column: "SucursalID");

            migrationBuilder.CreateIndex(
                name: "IX_CuentaPresupuesto_PresupuestoID",
                table: "CuentaPresupuesto",
                column: "PresupuestoID");

            migrationBuilder.CreateIndex(
                name: "IX_Presupuesto_SucursalID",
                table: "Presupuesto",
                column: "SucursalID");

            migrationBuilder.AddForeignKey(
                name: "FK_CuentaPresupuesto_Presupuesto_PresupuestoID",
                table: "CuentaPresupuesto",
                column: "PresupuestoID",
                principalTable: "Presupuesto",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListaMateriales_Sucursal_SucursalID",
                table: "ListaMateriales",
                column: "SucursalID",
                principalTable: "Sucursal",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SocioNegocio_Sucursal_SucursalID",
                table: "SocioNegocio",
                column: "SucursalID",
                principalTable: "Sucursal",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionContable_Sucursal_SucursalID",
                table: "TransaccionContable",
                column: "SucursalID",
                principalTable: "Sucursal",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CuentaPresupuesto_Presupuesto_PresupuestoID",
                table: "CuentaPresupuesto");

            migrationBuilder.DropForeignKey(
                name: "FK_ListaMateriales_Sucursal_SucursalID",
                table: "ListaMateriales");

            migrationBuilder.DropForeignKey(
                name: "FK_SocioNegocio_Sucursal_SucursalID",
                table: "SocioNegocio");

            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionContable_Sucursal_SucursalID",
                table: "TransaccionContable");

            migrationBuilder.DropTable(
                name: "Presupuesto");

            migrationBuilder.DropIndex(
                name: "IX_TransaccionContable_SucursalID",
                table: "TransaccionContable");

            migrationBuilder.DropIndex(
                name: "IX_SocioNegocio_SucursalID",
                table: "SocioNegocio");

            migrationBuilder.DropIndex(
                name: "IX_ListaMateriales_SucursalID",
                table: "ListaMateriales");

            migrationBuilder.DropIndex(
                name: "IX_CuentaPresupuesto_PresupuestoID",
                table: "CuentaPresupuesto");

            migrationBuilder.DropColumn(
                name: "SucursalID",
                table: "TransaccionContable");

            migrationBuilder.DropColumn(
                name: "SucursalID",
                table: "SocioNegocio");

            migrationBuilder.DropColumn(
                name: "SucursalID",
                table: "ListaMateriales");

            migrationBuilder.DropColumn(
                name: "PresupuestoID",
                table: "CuentaPresupuesto");
        }
    }
}
