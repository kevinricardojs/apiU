using System.ComponentModel.DataAnnotations;
using WebApiSistema.Models.Presupuesto;

namespace WebApiSistema.Models.Productos
{
    public class FamiliaProducto
    {
        [Key]
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public int CuentaID { get; set; }
        public Cuenta Cuenta { get; set; }
        public int CuentaIDO { get; set; }
    }
}
