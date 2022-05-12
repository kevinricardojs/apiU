using System.Threading.Tasks;
using WebApiSistema.DTO;
using WebApiSistema.DTO.Compras;

namespace WebApiSistema.Services.Transacciones
{
    public interface ITransaccionInventario
    {
        public Task<ResponseDTO> Ingreso(CompraCreate compra);
        public Task<ResponseDTO> Egreso(CompraCreate compra);
    }
}
