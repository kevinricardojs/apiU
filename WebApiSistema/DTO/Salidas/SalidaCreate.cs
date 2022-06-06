using System.Collections.Generic;

namespace WebApiSistema.DTO.Salidas
{
    public class SalidaCreate
    {
        public int SucursalID { get; set; }
        public string Comentarios { get; set; }
        public ICollection<SalidaCreateDetail> Detalles { get; set; }
    }
}
