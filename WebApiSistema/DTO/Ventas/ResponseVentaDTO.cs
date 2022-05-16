using WebApiSistema.Models.Venta;

namespace WebApiSistema.DTO.Ventas
{
    public class ResponseVentaDTO : ResponseDTO
    {
        public VentaCreateResponse Venta { get; set; }
    }
}
