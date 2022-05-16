using System;
using WebApiSistema.Models.Productos;

namespace WebApiSistema.DTO.Ventas
{
    public class VentaCreateResponseDetail
    {
        public int ID { get; set; }
        public int ProductoID { get; set; }
        public int NoLinea { get; set; }
        public Decimal Cantidad { get; set; }
        public Decimal Precio { get; set; }
        public string Descripcion { get; set; }
    }
}
