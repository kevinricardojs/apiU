using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSistema.DTO.Servicio
{
    public class ServicioCreate
    {
        public int SocioNegocioID { get; set; }
        public int SucursalID { get; set; }
        public DateTime FechaCreado { get; set; }
        public string Observaciones { get; set; }
        public ICollection<ServicioCreateDetalle> Detalles { get; set; }
    }
}
