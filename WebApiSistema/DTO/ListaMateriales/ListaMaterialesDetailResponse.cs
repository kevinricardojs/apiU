
namespace WebApiSistema.DTO.ListaMateriales
{
    public class ListaMaterialesDetailResponse
    {
        public int ID { get; set; }
        public int NoLinea { get; set; }
        public int ProductoID { get; set; }
        public string Instrucciones { get; set; }
        public decimal Cantidad { get; set; }
    }
}
