using System;
using System.Collections.Generic;

namespace WebApiSistema.DTO.ListaMateriales
{
    public class ListaMaterialesCreate
    {
        public int ProductoID { get; set; }
        public DateTime Creado { get; set; }
        public int SucursalID { get; set; }
        public string Instrucciones { get; set; }
        public decimal Cantidad { get; set; }
        public ICollection<ListaMaterialesDetailCreate> Materiales { get; set; }
    }
}
