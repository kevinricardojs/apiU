using System.ComponentModel.DataAnnotations;
using WebApiSistema.Models.Productos;

namespace WebApiSistema.Models.Produccion
{
    public class ProduccionDetalles
    {
        [Key]
        public int ID { get; set; }
        public int ProduccionID { get; set; }
        public Produccion Produccion { get; set; }
        public int NoLinea { get; set; }
        public int ProductoID { get; set; }
        public Producto Producto { get; set; }
        public decimal Cantidad { get; set; }
    }
}
