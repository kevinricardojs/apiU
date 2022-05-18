using System.Collections.Generic;

namespace WebApiSistema.DTO.Ventas
{
    public class VentaCreate
    {
        public int SocioNegocioID { get; set; }
        public int SucursalID { get; set; }
        public string FacturaSerie { get; set; }
        public ICollection<VentaCreateDetail> Detalles { get; set; }
    }
}
