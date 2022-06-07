
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApiSistema.Models.Productos;

namespace WebApiSistema.Models.Alquiler
{
    public class AlquilerDetalle
    {
        [Key]
        public int ID { get; set; }
        public int AlquilerID { get; set; }
        [JsonIgnore]
        public Alquiler Alquiler { get; set; }
        public int ProductoID { get; set; }
        [JsonIgnore]
        public Producto Producto { get; set; }
        public decimal Precio { get; set; }
        public int NoLinea { get; set; }
    }
}
