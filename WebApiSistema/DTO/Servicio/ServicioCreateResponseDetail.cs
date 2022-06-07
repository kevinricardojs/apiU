
namespace WebApiSistema.DTO.Servicio
{
    public class ServicioCreateResponseDetail
    {
        public int ID { get; set; }
        public int ServicioID { get; set; }
        public int ProductoID { get; set; }
        public decimal Precio { get; set; }
        public int NoLinea { get; set; }
    }
}
