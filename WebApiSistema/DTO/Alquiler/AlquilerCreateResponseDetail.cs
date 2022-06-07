
namespace WebApiSistema.DTO.Alquiler
{
    public class AlquilerCreateResponseDetail
    {
        public int ID { get; set; }
        public int AlquilerID { get; set; }
        public int ProductoID { get; set; }
        public decimal Precio { get; set; }
        public int NoLinea { get; set; }
    }
}
