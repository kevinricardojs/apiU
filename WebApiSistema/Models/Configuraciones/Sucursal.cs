using System.ComponentModel.DataAnnotations;

namespace WebApiSistema.Models.Configuraciones
{
    public class Sucursal
    {
        [Key]
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public int EmpresaID { get; set; }
        public Empresa Empresa { get; set; }
    }
}
