
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApiSistema.Models.Productos;

namespace WebApiSistema.Models.Servicio
{
    public class ServicioDetalle
    {
        [Key]
        public int ID { get; set; }
        public int ServicioID { get; set; }
        [JsonIgnore]
        public Servicio Servicio { get; set; }
        public int ProductoID { get; set; }
        [JsonIgnore]
        public Producto Producto { get; set; }
        public decimal Precio { get; set; }
        public int NoLinea { get; set; }
    }
}
