using System;
using System.ComponentModel.DataAnnotations;
using WebApiSistema.Models.Productos;

namespace WebApiSistema.Models.Venta
{
    public class VentaDetalle
    {
        [Key]
        public int ID { get; set; }
        public int VentaID { get; set; }
        public Venta Venta { get; set; }
        public int ProductoID { get; set; }
        public Producto Producto { get; set; }
        public int NoLinea { get; set; }
        public Decimal Cantidad { get; set; }
        public Decimal Precio { get; set; }
        public string Descripcion { get; set; }
    }
}
