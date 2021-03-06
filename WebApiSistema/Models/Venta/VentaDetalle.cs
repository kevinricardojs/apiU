using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiSistema.Models.Venta
{
    public class VentaDetalle
    {
        [Key]
        public int ID { get; set; }
        public int ProductoID { get; set; }
        public int NoLinea { get; set; }
        public Decimal Cantidad { get; set; }
        public Decimal Precio { get; set; }
        public string Descripcion { get; set; }
    }
}
