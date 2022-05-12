using System;

namespace WebApiSistema.DTO.Compras
{
    public class CompraCreateDetail
    {
        public int ProductoID { get; set; }
        public int NoLinea { get; set; }
        public Decimal Precio { get; set; }
        public string Descripcion { get; set; }
    }
}
