using System.Collections.Generic;


namespace WebApiSistema.DTO.Salidas
{
    public class SalidaCreateResponse
    {
        public int ID { get; set; }
        public int SucursalID { get; set; }
        public string Comentarios { get; set; }
        public ICollection<SalidaCreateResponseDetail> Detalles { get; set; }
    }
}
