using System.ComponentModel.DataAnnotations;
using WebApiSistema.Models.Configuraciones;

namespace WebApiSistema.Models.Productos
{
    public class ProductoTipo
    {
        [Key]
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public int SucursalID { get; set; }
        public Sucursal Sucursal { get; set; }
    }
}
