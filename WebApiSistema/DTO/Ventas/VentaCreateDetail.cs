using System;

namespace WebApiSistema.DTO.Ventas
{
    public class VentaCreateDetail
    {
        public int ProductoID { get; set; }
        public int NoLinea { get; set; }
        public Decimal Cantidad { get; set; }
        public Decimal Precio { get; set; }
        public string Descripcion { get; set; }
    }
}
