using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApiSistema.Models.Configuraciones;

namespace WebApiSistema.Models.Compra
{
    public class Compra
    {
        [Key]
        public int ID { get; set; }
        public int SocioNegocioID { get; set; }
        public SocioNegocio SocioNegocio { get; set; }
        public int SucursalID { get; set; }
        public Sucursal Sucursal { get; set; }
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public string FacturaSerie { get; set; }
        public DateTime FacturaFecha { get; set; }
        public ICollection<CompraDetalle> Detalles { get; set; }
    }
}
