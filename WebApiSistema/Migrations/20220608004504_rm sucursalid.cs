using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiSistema.Migrations
{
    public partial class rmsucursalid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CuentaPresupuesto_Sucursal_SucursalID",
                table: "CuentaPresupuesto");

            migrationBuilder.DropIndex(
                name: "IX_CuentaPresupuesto_SucursalID",
                table: "CuentaPresupuesto");

            migrationBuilder.DropColumn(
                name: "SucursalID",
                table: "CuentaPresupuesto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SucursalID",
                table: "CuentaPresupuesto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CuentaPresupuesto_SucursalID",
                table: "CuentaPresupuesto",
                column: "SucursalID");

            migrationBuilder.AddForeignKey(
                name: "FK_CuentaPresupuesto_Sucursal_SucursalID",
                table: "CuentaPresupuesto",
                column: "SucursalID",
                principalTable: "Sucursal",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
