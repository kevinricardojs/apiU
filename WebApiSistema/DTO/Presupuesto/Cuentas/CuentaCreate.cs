using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSistema.DTO.Presupuesto.Cuentas
{
    public class CuentaCreate
    {
        public string CodigoCuenta { get; set; }
        public int Nivel { get; set; }
        public string Descripcion { get; set; }
    }
}
