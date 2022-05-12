using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSistema.DTO.Compras
{
    public class CompraCreate
    {
        public int SocioNegocioID { get; set; }
        public string FacturaSerie { get; set; }
        public DateTime FacturaFecha { get; set; }
        public ICollection<CompraCreateDetail> Detalles { get; set; }
    }
}
