using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSistema.Data;
using WebApiSistema.DTO.Alquiler;
using WebApiSistema.DTO.Compras;
using WebApiSistema.DTO.Produccion;
using WebApiSistema.DTO.Salidas;
using WebApiSistema.DTO.Servicio;
using WebApiSistema.DTO.Ventas;
using WebApiSistema.Models.Alquiler;
using WebApiSistema.Models.Compra;
using WebApiSistema.Models.Helpers;
using WebApiSistema.Models.Presupuesto;
using WebApiSistema.Models.Produccion;
using WebApiSistema.Models.Salida;
using WebApiSistema.Models.Servicio;
using WebApiSistema.Models.Transacciones;
using WebApiSistema.Models.Venta;

namespace WebApiSistema.Services.Transacciones
{
    public class CTransaccionInventario : ITransaccionInventario
    {
        private readonly ApplicationDbContext _context;
        private readonly IDirectDB _directDB;
        private readonly IMapper _mapper;
        public CTransaccionInventario(ApplicationDbContext context, IDirectDB directDB, IMapper mapper)
        {
            _context = context;
            _directDB = directDB;
            _mapper = mapper;
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

                tc.SucursalID = venta.SucursalID;
                tc.FechaHora = tr.FechaHora;
                tc.Tipo = 0;
                tc.Detalles = new List<TransaccionDetalleContable>();

                tcCobro.SucursalID = venta.SucursalID;
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
                            Linea = ventaLinea.NoLinea,
                            Error = $"El Producto No Linea {ventaLinea.NoLinea} ID {stock.ProductoID} - {stock.Descripcion} no cuenta con suficiente stock"
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

                // Mapear venta generica a venta para BD
                Venta v = _mapper.Map<Venta>(venta);
                v.FechaHora = tr.FechaHora;

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

                var ventaCreada = _mapper.Map<VentaCreateResponse>(v);
                ResponseVentaDTO response = new ResponseVentaDTO
                {
                    Success = true,
                    Venta = ventaCreada
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
                tc.SucursalID = compra.SucursalID;


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
                            Linea = compraLinea.NoLinea,
                            Error = $"El Producto No Linea {compraLinea.NoLinea} ID {stock.ProductoID} - {stock.Descripcion} no cuenta con suficiente stock"
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

                // Mapear compra generica a compra para BD
                Compra c = _mapper.Map<Compra>(compra);
                c.FechaHora = tr.FechaHora;

                _context.Compra.Add(c);
                await _context.SaveChangesAsync();
                // Guardando la transaccion de la venta
                tr.CompraVentaID = c.ID;
                _context.TransaccionInventario.Add(tr);
                await _context.SaveChangesAsync();

                tc.CompraVentaID = c.ID;
                _context.TransaccionContable.Add(tc);
                await _context.SaveChangesAsync();

                transaction.Commit();
                var compraCreada = _mapper.Map<CompraCreateResponse>(c);
                ResponseCompraDTO response = new ResponseCompraDTO
                {
                    Success = true,
                    Compra = compraCreada
                };

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

        public async Task<ResponseProduccionDTO> Producir(ProduccionCreate produccion)
        {
            try
            {
                using var transaction = _context.Database.BeginTransaction();
                TransaccionInventario tr = new();
                TransaccionContable tc = new();

                tr.FechaHora = DateTime.Now;
                tr.Tipo = 2;
                tr.Detalles = new List<TransaccionDetalleInventario>();

                tc.SucursalID = produccion.SucursalID;
                tc.FechaHora = tr.FechaHora;
                tc.Tipo = 0;
                tc.Detalles = new List<TransaccionDetalleContable>();

                StockProducto productoCrear = await buscarStock(produccion.ListaMaterialesID + 10000);

                foreach (var detalleProduccion in produccion.Detalles)
                {
                    StockProducto stock = await buscarStock(detalleProduccion.ProductoID);
                    if (stock.ProductoID == detalleProduccion.ProductoID && stock.Total >= detalleProduccion.Cantidad)
                    {
                        // Detalle de inventario
                        decimal valorTransaccion = stock.PrecioPromedio * detalleProduccion.Cantidad;
                        tr.Detalles.Add(new TransaccionDetalleInventario
                        {

                            Salida = detalleProduccion.Cantidad,
                            Entrada = 0,
                            Linea = detalleProduccion.NoLinea,
                            ProductoID = detalleProduccion.ProductoID,
                            SucursalID = produccion.SucursalID,
                            FechaHora = tr.FechaHora,
                            Valor = valorTransaccion
                        });


                        // Cuenta Inventario PT
                        tc.Detalles.Add(new TransaccionDetalleContable
                        {
                            CuentaID = stock.CuentaID,
                            Debe = 0,
                            Haber = valorTransaccion,
                            FechaHora = tc.FechaHora,
                            Linea = detalleProduccion.NoLinea,
                            SucursalID = produccion.SucursalID
                        });


                    }
                    else
                    {
                        return new ResponseProduccionDTO
                        {
                            Success = false,
                            Linea = detalleProduccion.NoLinea,
                            Error = $"El Producto No Linea {detalleProduccion.NoLinea} ID {stock.ProductoID} - {stock.Descripcion} no cuenta con suficiente stock"
                        };
                    }

                }
                decimal totalProductos = tc.Detalles.Sum(x => x.Haber);

                /* Total a Producir*/
                tr.Detalles.Add(new TransaccionDetalleInventario
                {

                    Salida = 0,
                    Entrada = produccion.Cantidad,
                    Linea = tr.Detalles.Count - 1,
                    ProductoID = productoCrear.ProductoID,
                    SucursalID = produccion.SucursalID,
                    FechaHora = tr.FechaHora,
                    Valor = totalProductos
                });
                
                /* Total Valor en costos*/
                tc.Detalles.Add(new TransaccionDetalleContable
                {
                    CuentaID = productoCrear.CuentaID,
                    Debe = totalProductos,
                    Haber = 0,
                    FechaHora = tc.FechaHora,
                    Linea = tc.Detalles.Count - 1,
                    SucursalID = produccion.SucursalID
                });

                /* Fin calculo costo de venta*/


                // Mapear venta generica a venta para BD
                Produccion p = _mapper.Map<Produccion>(produccion);
                p.Creado = tr.FechaHora;

                _context.Produccion.Add(p);
                await _context.SaveChangesAsync();
                // Guardando la transaccion de la produccion
                tr.CompraVentaID = p.ID;
                _context.TransaccionInventario.Add(tr);
                await _context.SaveChangesAsync();

                tc.CompraVentaID = p.ID;
                _context.TransaccionContable.Add(tc);
                await _context.SaveChangesAsync();

                var produccionFinalizada = _mapper.Map<ProduccionCreateResponse>(p);
                ResponseProduccionDTO response = new ResponseProduccionDTO
                {
                    Success = true,
                    Produccion = produccionFinalizada
                };
                transaction.Commit();
                return response;
            }
            catch (Exception e)
            {
                return new ResponseProduccionDTO
                {
                    Success = false,
                    Error = $"Ha ocurrido un error Error:{e.Message}"
                };
            }

        }

        public async Task<ResponseSalidaDTO> SalidaStock(SalidaCreate salida)
        {
            try
            {
                using var transaction = _context.Database.BeginTransaction();
                TransaccionInventario tr = new();
                TransaccionContable tc = new();

                tr.FechaHora = DateTime.Now;
                tr.Tipo = 1;
                tr.Detalles = new List<TransaccionDetalleInventario>();

                tc.SucursalID = salida.SucursalID;
                tc.FechaHora = tr.FechaHora;
                tc.Tipo = 0;
                tc.Detalles = new List<TransaccionDetalleContable>();

                foreach (var linea in salida.Detalles)
                {
                    StockProducto stock = await buscarStock(linea.ProductoID);
                    if (stock.ProductoID == linea.ProductoID && stock.Total >= linea.Cantidad)
                    {
                        // Detalle de inventario
                        decimal valorTransaccion = stock.PrecioPromedio * linea.Cantidad;
                        tr.Detalles.Add(new TransaccionDetalleInventario
                        {

                            Salida = linea.Cantidad,
                            Entrada = 0,
                            Linea = linea.NoLinea,
                            ProductoID = linea.ProductoID,
                            SucursalID = salida.SucursalID,
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
                            Linea = linea.NoLinea,
                            SucursalID = salida.SucursalID
                        });
                        tc.Detalles.Add(new TransaccionDetalleContable
                        {
                            CuentaID = stock.CuentaIDO,
                            Debe = valorTransaccion,
                            Haber = 0,
                            FechaHora = tc.FechaHora,
                            Linea = linea.NoLinea,
                            SucursalID = salida.SucursalID
                        });
                    }
                    else
                    {
                        return new ResponseSalidaDTO
                        {
                            Success = false,
                            Linea = linea.NoLinea,
                            Error = $"El Producto No Linea {linea.NoLinea} ID {stock.ProductoID} - {stock.Descripcion} no cuenta con suficiente stock"
                        };
                    }

                }


                // Mapear venta generica a salida para BD
                Salida s = _mapper.Map<Salida>(salida);
                s.FechaHora = tr.FechaHora;

                _context.Salida.Add(s);
                await _context.SaveChangesAsync();
                // Guardando la transaccion de la salida
                tr.CompraVentaID = s.ID;
                _context.TransaccionInventario.Add(tr);
                await _context.SaveChangesAsync();

                tc.CompraVentaID = s.ID;
                _context.TransaccionContable.Add(tc);
                await _context.SaveChangesAsync();

                var salidaCreada = _mapper.Map<SalidaCreateResponse>(s);
                ResponseSalidaDTO response = new ResponseSalidaDTO
                {
                    Success = true,
                    Salida = salidaCreada
                };
                transaction.Commit();
                return response;
            }
            catch (Exception e)
            {
                return new ResponseSalidaDTO
                {
                    Success = false,
                    Error = $"Ha ocurrido un error Error:{e.Message}"
                };
            }

        }

        public async Task<ResponseAlquilerDTO> Alquiler(AlquilerCreate alquiler)
        {
            try
            {
                using var transaction = _context.Database.BeginTransaction();
                TransaccionContable tcCobro = new();

                tcCobro.SucursalID = alquiler.SucursalID;
                tcCobro.FechaHora = DateTime.Now;
                tcCobro.Tipo = 0;
                tcCobro.Detalles = new List<TransaccionDetalleContable>();

                foreach (var alquilerLinea in alquiler.Detalles)
                {
                    StockProducto stock = await buscarStock(alquilerLinea.ProductoID);

                        // Transaccion de pagar
                        tcCobro.Detalles.Add(new TransaccionDetalleContable
                        {
                            CuentaID = stock.CuentaIDO,
                            Debe = 0,
                            Haber = alquilerLinea.Precio,
                            FechaHora = tcCobro.FechaHora,
                            Linea = alquilerLinea.NoLinea,
                            SucursalID = alquiler.SucursalID
                        });
                }
                Cuenta cuentaIva = await CuentaIvaVenta();
                Cuenta cuentaBanco = await CuentaBanco();
                Cuenta cuentaCostoVenta = await CuentaCostoVenta();
                decimal total = alquiler.Detalles.Sum(x => x.Precio);
                decimal totalSinIva = total / (decimal)1.12;
                decimal totalProductos = tcCobro.Detalles.Sum(x => x.Haber);

                tcCobro.Detalles.Add(new TransaccionDetalleContable
                {
                    CuentaID = cuentaIva.ID,
                    Debe = 0,
                    Haber = total - totalSinIva,
                    FechaHora = tcCobro.FechaHora,
                    Linea = tcCobro.Detalles.Count,
                    SucursalID = alquiler.SucursalID
                });

                tcCobro.Detalles.Add(new TransaccionDetalleContable
                {
                    CuentaID = cuentaBanco.ID,
                    Debe = total,
                    Haber = 0,
                    FechaHora = tcCobro.FechaHora,
                    Linea = tcCobro.Detalles.Count,
                    SucursalID = alquiler.SucursalID
                });

                // Mapear venta generica a venta para BD
                Alquiler v = _mapper.Map<Alquiler>(alquiler);
                v.FechaCreado = tcCobro.FechaHora;

                _context.Alquiler.Add(v);
                await _context.SaveChangesAsync();

                tcCobro.CompraVentaID = v.ID;
                _context.TransaccionContable.Add(tcCobro);
                await _context.SaveChangesAsync();

                var alquilerCreada = _mapper.Map<AlquilerCreateResponse>(v);
                ResponseAlquilerDTO response = new ResponseAlquilerDTO
                {
                    Success = true,
                    Alquiler = alquilerCreada
                };
                transaction.Commit();
                return response;
            }
            catch (Exception e)
            {
                return new ResponseAlquilerDTO
                {
                    Success = false,
                    Error = $"Ha ocurrido un error Error:{e.Message}"
                };
            }

        }

        public async Task<ResponseServicioDTO> Servicio(ServicioCreate servicio)
        {
            try
            {
                using var transaction = _context.Database.BeginTransaction();
                TransaccionContable tcCobro = new();

                tcCobro.SucursalID = servicio.SucursalID;
                tcCobro.FechaHora = DateTime.Now;
                tcCobro.Tipo = 0;
                tcCobro.Detalles = new List<TransaccionDetalleContable>();

                foreach (var servicioLinea in servicio.Detalles)
                {
                    StockProducto stock = await buscarStock(servicioLinea.ProductoID);

                    // Transaccion de pagar
                    tcCobro.Detalles.Add(new TransaccionDetalleContable
                    {
                        CuentaID = stock.CuentaIDO,
                        Debe = 0,
                        Haber = servicioLinea.Precio,
                        FechaHora = tcCobro.FechaHora,
                        Linea = servicioLinea.NoLinea,
                        SucursalID = servicio.SucursalID
                    });
                }
                Cuenta cuentaIva = await CuentaIvaVenta();
                Cuenta cuentaBanco = await CuentaBanco();
                Cuenta cuentaCostoVenta = await CuentaCostoVenta();
                decimal total = servicio.Detalles.Sum(x => x.Precio);
                decimal totalSinIva = total / (decimal)1.12;
                decimal totalProductos = tcCobro.Detalles.Sum(x => x.Haber);

                tcCobro.Detalles.Add(new TransaccionDetalleContable
                {
                    CuentaID = cuentaIva.ID,
                    Debe = 0,
                    Haber = total - totalSinIva,
                    FechaHora = tcCobro.FechaHora,
                    Linea = tcCobro.Detalles.Count,
                    SucursalID = servicio.SucursalID
                });

                tcCobro.Detalles.Add(new TransaccionDetalleContable
                {
                    CuentaID = cuentaBanco.ID,
                    Debe = total,
                    Haber = 0,
                    FechaHora = tcCobro.FechaHora,
                    Linea = tcCobro.Detalles.Count,
                    SucursalID = servicio.SucursalID
                });

                // Mapear venta generica a venta para BD
                Servicio v = _mapper.Map<Servicio>(servicio);
                v.FechaCreado = tcCobro.FechaHora;

                _context.Servicio.Add(v);
                await _context.SaveChangesAsync();

                tcCobro.CompraVentaID = v.ID;
                _context.TransaccionContable.Add(tcCobro);
                await _context.SaveChangesAsync();

                var servicioCreada = _mapper.Map<ServicioCreateResponse>(v);
                ResponseServicioDTO response = new ResponseServicioDTO
                {
                    Success = true,
                    Servicio = servicioCreada
                };
                transaction.Commit();
                return response;
            }
            catch (Exception e)
            {
                return new ResponseServicioDTO
                {
                    Success = false,
                    Error = $"Ha ocurrido un error Error:{e.Message}"
                };
            }

        }
    }
}
