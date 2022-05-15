using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSistema.Data;
using WebApiSistema.DTO;
using WebApiSistema.DTO.Compras;
using WebApiSistema.DTO.Ventas;
using WebApiSistema.Models.Compra;
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
        public async Task<ResponseVentaDTO> Egreso(VentaCreate venta)
        {
            try
            {
                using var transaction = _context.Database.BeginTransaction();
                TransaccionInventario tr = new();
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
                    if (stock.ProductoID == ventaLinea.ProductoID && stock.Total >= ventaLinea.Cantidad)
                    {
                        // Detalle de inventario
                        decimal valorTransaccion = stock.PrecioPromedio * ventaLinea.Cantidad;
                        tr.Detalles.Add(new TransaccionDetalleInventario
                        {
                            
                            Salida = ventaLinea.Cantidad,
                            Entrada = 0,
                            Linea = ventaLinea.NoLinea,
                            ProductoID = ventaLinea.ProductoID,
                            SucursalID = venta.SucursalID,
                            FechaHora = tr.FechaHora,
                            Valor = valorTransaccion
                        });

                        // Transaccion de sacar de inventario
                        // Cuenta Inventario PT
                        tc.Detalles.Add(new TransaccionDetalleContable
                        {
                            CuentaID = stock.CuentaID,
                            Debe = 0,
                            Haber = valorTransaccion,
                            FechaHora = tc.FechaHora,
                            Linea = ventaLinea.NoLinea,
                            SucursalID = venta.SucursalID
                        });



                        // Transaccion de pagar
                        tcCobro.Detalles.Add(new TransaccionDetalleContable
                        {
                            CuentaID = stock.CuentaIDO,
                            Debe = 0,
                            Haber = ventaLinea.Precio / (decimal)1.12 * ventaLinea.Cantidad,
                            FechaHora = tcCobro.FechaHora,
                            Linea = ventaLinea.NoLinea,
                            SucursalID = venta.SucursalID
                        });
                    }
                    else
                    {
                        return new ResponseVentaDTO
                        {
                            Success = false,
                            Error = $"El Producto con ID {ventaLinea.ProductoID} no cuenta con suficiente stock"
                        };
                    }

                }
                Cuenta cuentaIva = await CuentaIvaVenta();
                Cuenta cuentaBanco = await CuentaBanco();
                Cuenta cuentaCostoVenta = await CuentaCostoVenta();
                decimal total = venta.Detalles.Sum(x => x.Precio * x.Cantidad);
                decimal totalSinIva = total / (decimal)1.12;
                decimal totalProductos = tc.Detalles.Sum(x => x.Haber);
                // Transaccion Costo de venta
                tc.Detalles.Add(new TransaccionDetalleContable
                {
                    CuentaID = cuentaCostoVenta.ID,
                    Debe = totalProductos,
                    Haber = 0,
                    FechaHora = tc.FechaHora,
                    Linea = venta.Detalles.Count,
                    SucursalID = venta.SucursalID
                });

                /* Fin calculo costo de venta*/

                /* Venta de PT */
                tcCobro.Detalles.Add(new TransaccionDetalleContable
                {
                    CuentaID = cuentaIva.ID,
                    Debe = 0,
                    Haber = total - totalSinIva,
                    FechaHora = tc.FechaHora,
                    Linea = tc.Detalles.Count,
                    SucursalID = venta.SucursalID
                });

                tcCobro.Detalles.Add(new TransaccionDetalleContable
                {
                    CuentaID = cuentaBanco.ID,
                    Debe = total,
                    Haber = 0,
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
                        Descripcion = detalle.Descripcion,
                        Cantidad = detalle.Cantidad
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


                ResponseVentaDTO response = new ResponseVentaDTO
                {
                    Success = true,
                    Venta = v
                };
                transaction.Commit();
                return response;
            }
            catch(Exception e)
            {
                return new ResponseVentaDTO
                {
                    Success = false,
                    Error = $"Ha ocurrido un error Error:{e.Message}"
                };
            }
            
        }

        public async Task<ResponseCompraDTO> Ingreso(CompraCreate compra)
        {
            try
            {
                using var transaction = _context.Database.BeginTransaction();
                TransaccionInventario tr = new();
                TransaccionContable tc = new();


                tr.FechaHora = DateTime.Now;
                tr.Tipo = 1;
                tr.Detalles = new List<TransaccionDetalleInventario>();

                tc.FechaHora = tr.FechaHora;
                tc.Tipo = 0;
                tc.Detalles = new List<TransaccionDetalleContable>();

                foreach (var compraLinea in compra.Detalles)
                {
                    StockProducto stock = await buscarStock(compraLinea.ProductoID);
                    if (stock.ProductoID == compraLinea.ProductoID)
                    {
                        // Detalle de inventario
                        decimal valorTransaccion = compraLinea.Precio / (decimal)1.12 * compraLinea.Cantidad;
                        tr.Detalles.Add(new TransaccionDetalleInventario
                        {
                            Salida = 0,
                            Entrada = compraLinea.Cantidad,
                            Linea = compraLinea.NoLinea,
                            ProductoID = compraLinea.ProductoID,
                            SucursalID = compra.SucursalID,
                            FechaHora = tr.FechaHora,
                            Valor = valorTransaccion
                        });

                        // Transaccion de ingresar de inventario
                        // Cuenta Inventario PT
                        tc.Detalles.Add(new TransaccionDetalleContable
                        {
                            CuentaID = stock.CuentaID,
                            Debe = compraLinea.Precio / (decimal)1.12 * compraLinea.Cantidad,
                            Haber = 0,
                            FechaHora = tc.FechaHora,
                            Linea = compraLinea.NoLinea,
                            SucursalID = compra.SucursalID
                        });
                    }
                    else
                    {
                        return new ResponseCompraDTO
                        {
                            Success = false,
                            Error = $"El Producto con ID {compraLinea.ProductoID} no cuenta con suficiente stock"
                        };
                    }

                }
                Cuenta cuentaIvaCompra = await CuentaIvaCompra();
                Cuenta cuentaBanco = await CuentaBanco();
                Cuenta cuentaCostoVenta = await CuentaCostoVenta();
                decimal total = compra.Detalles.Sum(x => x.Precio * x.Cantidad);
                decimal totalSinIva = total / (decimal)1.12;
                // Transaccion Costo de compra
                tc.Detalles.Add(new TransaccionDetalleContable
                {
                    CuentaID = cuentaIvaCompra.ID,
                    Debe = total - totalSinIva,
                    Haber = 0,
                    FechaHora = tc.FechaHora,
                    Linea = compra.Detalles.Count,
                    SucursalID = compra.SucursalID
                });

                tc.Detalles.Add(new TransaccionDetalleContable
                {
                    CuentaID = cuentaBanco.ID,
                    Debe = 0,
                    Haber = total,
                    FechaHora = tc.FechaHora,
                    Linea = tc.Detalles.Count,
                    SucursalID = compra.SucursalID
                });

                /* Fin Costo de compra*/

                List<CompraDetalle> detalles = new List<CompraDetalle>();

                foreach (var detalle in compra.Detalles)
                {
                    detalles.Add(new CompraDetalle
                    {
                        NoLinea = detalle.NoLinea,
                        ProductoID = detalle.ProductoID,
                        Precio = detalle.Precio,
                        Descripcion = detalle.Descripcion,
                        Cantidad = detalle.Cantidad
                    });
                }

                Compra c = new Compra
                {
                    SocioNegocioID = compra.SocioNegocioID,
                    FacturaSerie = compra.FacturaSerie,
                    FacturaFecha = compra.FacturaFecha,
                    Detalles = detalles
                };
                _context.Compra.Add(c);
                await _context.SaveChangesAsync();
                // Guardando la transaccion de la venta
                tr.CompraVentaID = c.ID;
                _context.TransaccionInventario.Add(tr);
                await _context.SaveChangesAsync();

                tc.CompraVentaID = c.ID;
                _context.TransaccionContable.Add(tc);
                await _context.SaveChangesAsync();


                ResponseCompraDTO response = new ResponseCompraDTO
                {
                    Success = true,
                    Compra = c
                };
                transaction.Commit();
                return response;
            }
            catch (Exception e)
            {
                return new ResponseCompraDTO
                {
                    Success = false,
                    Error = $"Ha ocurrido un error Error:{e.Message}"
                };
            }
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

        public async Task<Cuenta> CuentaIvaCompra()
        {
            Cuenta cuenta = new Cuenta();
            string query = "CALL CuentaIvaCompra()";
            var stock = await _directDB.GetData(query);
            if (stock.Count > 0)
            {
                string json = JsonConvert.SerializeObject(stock);
                cuenta = JsonConvert.DeserializeObject<Cuenta>(json);
            }
            return cuenta;
        }

        public async Task<Cuenta> CuentaBanco()
        {
            Cuenta cuenta = new Cuenta();
            string query = "CALL CuentaBanco()";
            var stock = await _directDB.GetData(query);
            if (stock.Count > 0)
            {
                string json = JsonConvert.SerializeObject(stock);
                cuenta = JsonConvert.DeserializeObject<Cuenta>(json);
            }
            return cuenta;
        }

        public async Task<Cuenta> CuentaCostoVenta()
        {
            Cuenta cuenta = new Cuenta();
            string query = "CALL CuentaCostoVenta()";
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
