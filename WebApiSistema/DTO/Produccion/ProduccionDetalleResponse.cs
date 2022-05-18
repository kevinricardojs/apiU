using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSistema.DTO.Produccion
{
    public class ProduccionDetalleResponse
    {
        public int ID { get; set; }
        public int ProduccionID { get; set; }
        public int NoLinea { get; set; }
        public int ProductoID { get; set; }
        public decimal Cantidad { get; set; }
    }
}
