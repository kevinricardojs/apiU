using System.Threading.Tasks;
using WebApiSistema.DTO;
using WebApiSistema.DTO.Compras;
using WebApiSistema.DTO.Produccion;
using WebApiSistema.DTO.Salidas;
using WebApiSistema.DTO.Ventas;

namespace WebApiSistema.Services.Transacciones
{
    public interface ITransaccionInventario
    {
        public Task<ResponseCompraDTO> Ingreso(CompraCreate compra);
        public Task<ResponseVentaDTO> Egreso(VentaCreate compra);
        public Task<ResponseProduccionDTO> Producir(ProduccionCreate produccion);
        public Task<ResponseSalidaDTO> SalidaStock(SalidaCreate salida);
    }
}
