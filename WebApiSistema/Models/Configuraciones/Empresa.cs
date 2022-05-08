using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiSistema.Models.Configuraciones
{
    public class Empresa
    {
        [Key]
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public ICollection<Sucursal> Sucursales { get; set; }
    }
}
