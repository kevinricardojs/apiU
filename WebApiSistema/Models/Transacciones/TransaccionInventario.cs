using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiSistema.Models.Transacciones
{
    public class TransaccionInventario
    {
        [Key]
        public int ID { get; set; }
        public DateTime FechaHora { get; set; }
        public int Tipo { get; set; }
        public int CompraVentaID { get; set; }
        public ICollection<TransaccionDetalleInventario> Detalles { get; set; }
    }
}
