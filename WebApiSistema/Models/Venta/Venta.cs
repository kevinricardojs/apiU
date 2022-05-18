using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApiSistema.Models.Configuraciones;

namespace WebApiSistema.Models.Venta
{
    public class Venta
    {
        [Key]
        public int ID { get; set; }
        public int SocioNegocioID { get; set; }
        public SocioNegocio SocioNegocio { get; set; }
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public string FacturaSerie { get; set; }
        public DateTime FacturaFecha { get; set; }
        public ICollection<VentaDetalle> Detalles { get; set; }
    }
}
