using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiSistema.Models.Produccion
{
    public class Produccion
    {
        [Key]
        public int ID { get; set; }
        public int ListaMaterialesID { get; set; }
        public ListaMateriales ListaMateriales { get; set; }
        public DateTime Creado { get; set; } = DateTime.Now;
        public decimal Cantidad { get; set; }
        public ICollection<ProduccionDetalles> Detalles { get; set; }
    }
}
