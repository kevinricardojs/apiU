using WebApiSistema.Models.Compra;

namespace WebApiSistema.DTO.Compras
{
    public class ResponseCompraDTO : ResponseDTO
    {
        public Compra Compra { get; set; }
    }
}
