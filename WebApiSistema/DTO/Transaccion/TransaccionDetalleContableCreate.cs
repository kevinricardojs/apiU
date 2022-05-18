using System;

namespace WebApiSistema.DTO.Transaccion
{
    public class TransaccionDetalleContableCreate
    {
        public int CuentaID { get; set; }
        public int SucursalID { get; set; }
        public int Linea { get; set; }
        public Decimal Debe { get; set; }
        public Decimal Haber { get; set; }
    }
}
