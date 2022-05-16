using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSistema.DTO.ListaMateriales
{
    public class ListaMaterialesResponse
    {
        public int ID { get; set; }
        public int ProductoID { get; set; }
        public DateTime Creado { get; set; }
        public string Instrucciones { get; set; }
        public decimal Cantidad { get; set; }
        public ICollection<ListaMaterialesDetailResponse> Materiales { get; set; }
    }
}
