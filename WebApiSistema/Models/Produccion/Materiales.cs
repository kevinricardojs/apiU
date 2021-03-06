using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApiSistema.Models.Productos;

namespace WebApiSistema.Models.Produccion
{
    public class Materiales
    {
        [Key]
        public int ID { get; set; }
        public int ListaMaterialesID { get; set; }
        [JsonIgnore]
        public ListaMateriales ListaMateriales { get; set; }
        public int NoLinea { get; set; }
        public int ProductoID { get; set; }
        [JsonIgnore]
        public Producto Producto { get; set; }
        public string Instrucciones { get; set; }
        public decimal Cantidad { get; set; }
    }
}
