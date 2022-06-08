using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSistema.DTO.Presupuesto
{
    public class PresupuestoDTO
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public DateTime Creado { get; set; }
    }
}
