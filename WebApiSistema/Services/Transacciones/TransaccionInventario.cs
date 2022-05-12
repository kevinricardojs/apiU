using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSistema.Data;
using WebApiSistema.DTO;
using WebApiSistema.DTO.Compras;
using WebApiSistema.Models.Helpers;
using WebApiSistema.Models.Productos;

namespace WebApiSistema.Services.Transacciones
{
    public class TransaccionInventario : ITransaccionInventario
    {
        private readonly ApplicationDbContext _context;
        private readonly IDirectDB _directDB;
        public TransaccionInventario(ApplicationDbContext context, IDirectDB directDB)
        {
            _context = context;
            _directDB = directDB;
        }
        public Task<ResponseDTO> Egreso(CompraCreate compra)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDTO> Ingreso(CompraCreate compra)
        {
            using var transaction = _context.Database.BeginTransaction();
            foreach(var producto in compra.Detalles)
            {
                string query = "CALL StockProducto(" + producto.ProductoID + ")";
                var stock = await _directDB.GetData(query);
                if (stock.Count > 0)
                {
                    string json = JsonConvert.SerializeObject(stock);
                    StockProducto stockProducto = JsonConvert.DeserializeObject<StockProducto>(json);
                }
            }
            
            ResponseDTO response = new ResponseDTO();
            
            return response;
        }
    }
}
