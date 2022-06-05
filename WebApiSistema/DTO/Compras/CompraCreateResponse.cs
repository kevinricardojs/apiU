using System;
using System.Collections.Generic;

namespace WebApiSistema.DTO.Compras
{
    public class CompraCreateResponse
    {
        public int ID { get; set; }
        public int SocioNegocioID { get; set; }
        public int SucursalID { get; set; }
        public DateTime FechaHora { get; set; }
        public string FacturaSerie { get; set; }
        public DateTime FacturaFecha { get; set; }
        public ICollection<CompraCreateResponseDetail> Detalles { get; set; }
    }
}
