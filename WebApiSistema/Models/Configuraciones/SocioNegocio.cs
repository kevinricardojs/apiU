using System.ComponentModel.DataAnnotations;

namespace WebApiSistema.Models.Configuraciones
{
    public class SocioNegocio
    {
        [Key]
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Nit { get; set; }
    }
}
