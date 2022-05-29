using System.ComponentModel.DataAnnotations;
using WebApiSistema.Models.Configuraciones;

namespace WebApiSistema.Models.Usuario
{
    public class UsuarioSucursal
    {
        [Key]
        public int ID { get; set; }
        public int UsuarioID { get; set; }
        public int SucursalID { get; set; }
        public Sucursal Sucursal { get; set; }
    }
}
