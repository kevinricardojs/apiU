using System;

namespace WebApiSistema.Models.Helpers
{
    public class StockProducto
    {
        public int ProductoID { get; set; } = 0;
        public Decimal Total { get; set; } = -1;
        public int CuentaID { get; set; }
        public int CuentaIDO { get; set; }
        public Decimal PrecioPromedio { get; set; }
        public string Descripcion { get; set; }
    }
}
