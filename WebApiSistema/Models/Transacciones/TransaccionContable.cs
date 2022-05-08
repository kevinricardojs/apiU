using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiSistema.Models.Transacciones
{
    public class TransaccionContable
    {
        [Key]
        public int ID { get; set; }
        public DateTime FechaHora { get; set; }
        public int Tipo { get; set; }
        public int CompraVentaID { get; set; }
    }
}
