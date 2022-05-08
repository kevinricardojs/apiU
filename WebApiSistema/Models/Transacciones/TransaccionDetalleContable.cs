using System;
using System.ComponentModel.DataAnnotations;
using WebApiSistema.Models.Configuraciones;
using WebApiSistema.Models.Presupuesto;

namespace WebApiSistema.Models.Transacciones
{
    public class TransaccionDetalleContable
    {
        [Key]
        public int ID { get; set; }
        public int CuentaID { get; set; }
        public Cuenta Cuenta { get; set; }
        public int SucursalID { get; set; }
        public Sucursal Sucursal { get; set; }
        public int Linea { get; set; }
        public Decimal Debe { get; set; }
        public Decimal Haber { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
