using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApiSistema.Models.Productos;

namespace WebApiSistema.Models.Salida
{
    public class SalidaDetalle
    {
        [Key]
        public int ID { get; set; }
        public int SalidaID { get; set; }
        [JsonIgnore]
        public Salida Salida { get; set; }
        public int NoLinea { get; set; }
        public int ProductoID { get; set; }
        [JsonIgnore]
        public Producto Producto { get; set; }
        public decimal Cantidad { get; set; }
    }
}
