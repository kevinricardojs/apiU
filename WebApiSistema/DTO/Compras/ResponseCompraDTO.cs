using WebApiSistema.Models.Compra;

namespace WebApiSistema.DTO.Compras
{
    public class ResponseCompraDTO : ResponseDTO
    {
        public CompraCreateResponse Compra { get; set; }
    }
}
