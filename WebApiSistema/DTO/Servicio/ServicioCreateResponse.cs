using System;
using System.Collections.Generic;

namespace WebApiSistema.DTO.Servicio
{
    public class ServicioCreateResponse
    {
        public int ID { get; set; }
        public int SocioNegocioID { get; set; }
        public int SucursalID { get; set; }
        public DateTime FechaCreado { get; set; }
        public string Observaciones { get; set; }
        public ICollection<ServicioCreateResponseDetail> Detalles { get; set; }
    }
}
