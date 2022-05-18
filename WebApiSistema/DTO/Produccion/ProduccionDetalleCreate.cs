
namespace WebApiSistema.DTO.Produccion
{
    public class ProduccionDetalleCreate
    {
        public int NoLinea { get; set; }
        public int ProductoID { get; set; }
        public decimal Cantidad { get; set; }
    }
}
