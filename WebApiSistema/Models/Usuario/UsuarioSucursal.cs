using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApiSistema.Models.Configuraciones;

namespace WebApiSistema.Models.Usuario
{
    public class UsuarioSucursal
    {
        [Key]
        public int ID { get; set; }
        public int UsuarioID { get; set; }
        public int SucursalID { get; set; }
        [JsonIgnore]
        public Sucursal Sucursal { get; set; }
    }
}
