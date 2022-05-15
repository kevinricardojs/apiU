using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSistema.DTO.Ventas
{
    public class VentaCreate
    {
        public int SocioNegocioID { get; set; }
        public int SucursalID { get; set; }
        public string FacturaSerie { get; set; }
        public DateTime FacturaFecha { get; set; }
        public ICollection<VentaCreateDetail> Detalles { get; set; }
    }
}
