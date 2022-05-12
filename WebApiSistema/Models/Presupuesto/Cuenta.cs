using System.ComponentModel.DataAnnotations;

namespace WebApiSistema.Models.Presupuesto
{
    public class Cuenta
    {
        [Key]
        public int ID { get; set; }
        public string CodigoCuenta { get; set; }
        public int Nivel { get; set; }
        public string Descripcion { get; set; }
    }
}
