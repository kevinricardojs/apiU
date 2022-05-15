using WebApiSistema.Models.Venta;

namespace WebApiSistema.DTO.Ventas
{
    public class ResponseVentaDTO : ResponseDTO
    {
        public Venta Venta { get; set; }
    }
}
