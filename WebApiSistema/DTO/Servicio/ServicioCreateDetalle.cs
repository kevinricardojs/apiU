
namespace WebApiSistema.DTO.Servicio
{
    public class ServicioCreateDetalle
    {
        public int ServicioID { get; set; }
        public int ProductoID { get; set; }
        public decimal Precio { get; set; }
        public int NoLinea { get; set; }
    }
}
