using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSistema.DTO.Salidas
{
    public class SalidaCreateResponseDetail
    {
        public int ID { get; set; }
        public int SalidaID { get; set; }
        public int NoLinea { get; set; }
        public int ProductoID { get; set; }
        public decimal Cantidad { get; set; }
    }
}
