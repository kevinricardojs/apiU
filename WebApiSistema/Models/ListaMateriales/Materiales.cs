using System.ComponentModel.DataAnnotations;
using WebApiSistema.Models.Productos;

namespace WebApiSistema.Models.ListaMateriales
{
    public class Materiales
    {
        [Key]
        public int ID { get; set; }
        public int ListaMaterialesID { get; set; }
        public ListaMateriales ListaMateriales { get; set; }
        public int NoLinea { get; set; }
        public int ProductoID { get; set; }
        public Producto Producto { get; set; }
        public string Instrucciones { get; set; }
        public decimal Cantidad { get; set; }
    }
}
