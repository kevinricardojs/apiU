using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSistema.Data;
using WebApiSistema.DTO;
using WebApiSistema.DTO.Compras;
using WebApiSistema.DTO.Ventas;
using WebApiSistema.Models.Helpers;
using WebApiSistema.Models.Presupuesto;
using WebApiSistema.Models.Transacciones;
using WebApiSistema.Models.Venta;

namespace WebApiSistema.Services.Transacciones
{
    public class CTransaccionInventario : ITransaccionInventario
    {
        private readonly ApplicationDbContext _context;
        private readonly IDirectDB _directDB;
        public CTransaccionInventario(ApplicationDbContext context, IDirectDB directDB)
        {
            _context = context;
            _directDB = directDB;
        }
        public async Task<ResponseDTO> Egreso(VentaCreate venta)
        {
            
            using var transaction = _context.Database.BeginTransaction();
            TransaccionInventario tr = new ();
            TransaccionContable tc = new();
            TransaccionContable tcCobro = new();


            tr.FechaHora = DateTime.Now;
            tr.Tipo = 1;
            tr.Detalles = new List<TransaccionDetalleInventario>();

            tc.FechaHora = tr.FechaHora;
            tc.Tipo = 0;
            tc.Detalles = new List<TransaccionDetalleContable>();

            tcCobro.FechaHora = tr.FechaHora;
            tcCobro.Tipo = 0;
            tcCobro.Detalles = new List<TransaccionDetalleContable>();

            foreach (var ventaLinea in venta.Detalles)
            {
                StockProducto stock = await buscarStock(ventaLinea.ProductoID);
                if(stock.ProductoID == ventaLinea.ProductoID && stock.Total >= ventaLinea.Cantidad)
                {
                    // Detalle de inventario
                    tr.Detalles.Add(new TransaccionDetalleInventario
                    {
                        Salida = ventaLinea.Cantidad,
                        Entrada = 0,
                        Linea = ventaLinea.NoLinea,
                        ProductoID = ventaLinea.ProductoID,
                        SucursalID = venta.SucursalID,
                        FechaHora = tr.FechaHora
                    });
                    // Transaccion de sacar de inventario
                    tc.Detalles.Add(new TransaccionDetalleContable
                    {
                        CuentaID = stock.CuentaID,
                        Debe = ventaLinea.Precio,
                        Haber = 0,
                        FechaHora = tc.FechaHora,
                        Linea = ventaLinea.NoLinea,
                        SucursalID = venta.SucursalID
                    });
                    // Transaccion de vender
                    tc.Detalles.Add(new TransaccionDetalleContable
                    {
                        CuentaID = stock.CuentaIDO,
                        Haber = ventaLinea.Precio - ventaLinea.Precio * (decimal)0.12,
                        Debe = 0,
                        FechaHora = tc.FechaHora,
                        Linea = venta.Detalles.Count +  ventaLinea.NoLinea,
                        SucursalID = venta.SucursalID
                    });
                    // Transaccion de pagar

                    tcCobro.Detalles.Add(new TransaccionDetalleContable
                    {
                        CuentaID = stock.CuentaIDO,
                        Haber = 0,
                        Debe = ventaLinea.Precio - ventaLinea.Precio * (decimal)0.12,
                        FechaHora = tcCobro.FechaHora,
                        Linea = ventaLinea.NoLinea,
                        SucursalID = venta.SucursalID
                    });
                }
                else{
                    return new ResponseDTO
                    {
                        Success = false,
                        Error = $"El Producto con ID {ventaLinea.ProductoID} no cuenta con suficiente stock"
                    };
                }

            }
            Cuenta cuentaIva = await CuentaIvaVenta();
            Cuenta cuentaBanco = await CuentaBancoVenta();
            decimal iva = Convert.ToDecimal(0.12);
            decimal totalIvaVenta = venta.Detalles.Sum(x => x.Precio) * iva;
            tc.Detalles.Add(new TransaccionDetalleContable
            {
                CuentaID = cuentaIva.ID,
                Haber = totalIvaVenta,
                Debe = 0,
                FechaHora = tc.FechaHora,
                Linea = tc.Detalles.Count,
                SucursalID = venta.SucursalID
            });

            decimal total = venta.Detalles.Sum(x => x.Precio);
            tcCobro.Detalles.Add(new TransaccionDetalleContable
            {
                CuentaID = cuentaIva.ID,
                Haber = 0,
                Debe = totalIvaVenta,
                FechaHora = tcCobro.FechaHora,
                Linea = tcCobro.Detalles.Count,
                SucursalID = venta.SucursalID
            });
            tcCobro.Detalles.Add(new TransaccionDetalleContable
            {
                CuentaID = cuentaBanco.ID,
                Haber = total,
                Debe = 0,
                FechaHora = tcCobro.FechaHora,
                Linea = tcCobro.Detalles.Count,
                SucursalID = venta.SucursalID
            });
            // Crea Linea de iva sobre de venta



            List<VentaDetalle> detalles = new List<VentaDetalle>();

            foreach (var detalle in venta.Detalles)
            {
                detalles.Add(new VentaDetalle
                {
                    NoLinea = detalle.NoLinea,
                    ProductoID = detalle.ProductoID,
                    Precio = detalle.Precio,
                    Descripcion = detalle.Descripcion
                });
            }

            Venta v = new Venta
            {
                SocioNegocioID = venta.SocioNegocioID,
                FacturaSerie = venta.FacturaSerie,
                FacturaFecha = venta.FacturaFecha,
                Detalles = detalles
            };
            _context.Venta.Add(v);
            await _context.SaveChangesAsync();
            // Guardando la transaccion de la venta
            tr.CompraVentaID = v.ID;
            _context.TransaccionInventario.Add(tr);
            await _context.SaveChangesAsync();

            tc.CompraVentaID = v.ID;
            _context.TransaccionContable.Add(tc);
            await _context.SaveChangesAsync();

            tcCobro.CompraVentaID = v.ID;
            _context.TransaccionContable.Add(tcCobro);
            await _context.SaveChangesAsync();


            ResponseDTO response = new ResponseDTO
            { 
                Success = true,
                Venta = v
            };
            transaction.Commit();
            return response;
        }

        public async Task<ResponseDTO> Ingreso(CompraCreate compra)
        {
            using var transaction = _context.Database.BeginTransaction();
            foreach (var producto in compra.Detalles)
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

        public async Task<StockProducto> buscarStock(int ProductoID)
        {
            StockProducto stockProducto = new StockProducto();
            string query = "CALL StockProducto(" + ProductoID + ")";
            var stock = await _directDB.GetData(query);
            if (stock.Count > 0)
            {
                string json = JsonConvert.SerializeObject(stock);
                stockProducto = JsonConvert.DeserializeObject<StockProducto>(json);
            }
            return stockProducto;
        }

        public async Task<Cuenta> CuentaIvaVenta()
        {
            Cuenta cuenta = new Cuenta();
            string query = "CALL CuentaIvaVenta()";
            var stock = await _directDB.GetData(query);
            if (stock.Count > 0)
            {
                string json = JsonConvert.SerializeObject(stock);
                cuenta = JsonConvert.DeserializeObject<Cuenta>(json);
            }
            return cuenta;
        }

        public async Task<Cuenta> CuentaBancoVenta()
        {
            Cuenta cuenta = new Cuenta();
            string query = "CALL CuentaBancoVenta()";
            var stock = await _directDB.GetData(query);
            if (stock.Count > 0)
            {
                string json = JsonConvert.SerializeObject(stock);
                cuenta = JsonConvert.DeserializeObject<Cuenta>(json);
            }
            return cuenta;
        }
    }
}
