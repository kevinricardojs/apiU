using System.Threading.Tasks;
using WebApiSistema.DTO.Alquiler;
using WebApiSistema.DTO.Compras;
using WebApiSistema.DTO.Produccion;
using WebApiSistema.DTO.Salidas;
using WebApiSistema.DTO.Servicio;
using WebApiSistema.DTO.Ventas;

namespace WebApiSistema.Services.Transacciones
{
    public interface ITransaccionInventario
    {
        public Task<ResponseCompraDTO> Ingreso(CompraCreate compra);
        public Task<ResponseVentaDTO> Egreso(VentaCreate compra);
        public Task<ResponseProduccionDTO> Producir(ProduccionCreate produccion);
        public Task<ResponseSalidaDTO> SalidaStock(SalidaCreate salida);
        public Task<ResponseAlquilerDTO> Alquiler(AlquilerCreate alquiler);
        public Task<ResponseServicioDTO> Servicio(ServicioCreate servicio);
    }
}
