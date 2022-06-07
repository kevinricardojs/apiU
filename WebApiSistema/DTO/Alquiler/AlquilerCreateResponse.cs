using System;
using System.Collections.Generic;

namespace WebApiSistema.DTO.Alquiler
{
    public class AlquilerCreateResponse
    {
        public int ID { get; set; }
        public int SocioNegocioID { get; set; }
        public int SucursalID { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public DateTime FechaCreado { get; set; }
        public string Observaciones { get; set; }
        public ICollection<AlquilerCreateResponseDetail> Detalles { get; set; }
    }
}
