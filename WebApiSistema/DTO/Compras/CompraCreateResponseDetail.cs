using System;

namespace WebApiSistema.DTO.Compras
{
    public class CompraCreateResponseDetail
    {
        public int ID { get; set; }
        public int ProductoID { get; set; }
        public int NoLinea { get; set; }
        public Decimal Cantidad { get; set; }
        public Decimal Precio { get; set; }
        public string Descripcion { get; set; }
    }
}
