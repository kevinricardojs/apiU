using System;

namespace WebApiSistema.DTO.Presupuesto.CuentasPresupuesto
{
    public class CuentaPresupuestoCreate
    {
        public int CuentaID { get; set; }
        public int SucursalID { get; set; }
        public Decimal Presupuesto { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
    }
}
