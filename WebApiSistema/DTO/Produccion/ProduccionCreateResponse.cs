using System;
using System.Collections.Generic;

namespace WebApiSistema.DTO.Produccion
{
    public class ProduccionCreateResponse
    {
        public int ID { get; set; }
        public int ListaMaterialesID { get; set; }
        public DateTime Creado { get; set; }
        public decimal Cantidad { get; set; }
        public ICollection<ProduccionDetalleResponse> Detalles { get; set; }
    }
}
