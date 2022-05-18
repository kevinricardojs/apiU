using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSistema.DTO.Produccion
{
    public class ProduccionCreate
    {
        public int SucursalID { get; set; }
        public int ListaMaterialesID { get; set; }
        public decimal Cantidad { get; set; }
        public ICollection<ProduccionDetalleCreate> Detalles { get; set; }
    }
}
