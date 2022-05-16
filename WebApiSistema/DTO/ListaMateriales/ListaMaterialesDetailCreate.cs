using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSistema.DTO.ListaMateriales
{
    public class ListaMaterialesDetailCreate
    {
        public int NoLinea { get; set; }
        public int ProductoID { get; set; }
        public string Instrucciones { get; set; }
        public decimal Cantidad { get; set; }
    }
}
