
namespace WebApiSistema.DTO.Alquiler
{
    public class AlquilerCreateDetalle
    {
        public int AlquilerID { get; set; }
        public int ProductoID { get; set; }
        public decimal Precio { get; set; }
        public int NoLinea { get; set; }
    }
}
