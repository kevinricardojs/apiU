using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSistema.DTO.Alquiler
{
    public class AlquilerCreate
    {
        public int SocioNegocioID { get; set; }
        public int SucursalID { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public DateTime FechaCreado { get; set; }
        public string Observaciones { get; set; }
        public ICollection<AlquilerCreateDetalle> Detalles { get; set; }
    }
}
