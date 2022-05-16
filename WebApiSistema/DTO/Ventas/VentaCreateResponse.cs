using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSistema.DTO.Ventas
{
    public class VentaCreateResponse
    {
        public int ID { get; set; }
        public int SocioNegocioID { get; set; }
        public DateTime FechaHora { get; set; }
        public string FacturaSerie { get; set; }
        public DateTime FacturaFecha { get; set; }
        public ICollection<VentaCreateResponseDetail> Detalles { get; set; }
    }
}
