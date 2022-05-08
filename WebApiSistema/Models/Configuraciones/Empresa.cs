using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSistema.Models.Configuraciones
{
    public class Empresa
    {
        [Key]
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }

    }
}
