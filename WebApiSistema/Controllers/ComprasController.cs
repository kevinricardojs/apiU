﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSistema.Data;
using WebApiSistema.DTO.Compras;
using WebApiSistema.Models.Compra;
using WebApiSistema.Services.Transacciones;

namespace WebApiSistema.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITransaccionInventario _trInventario;

        public ComprasController(ApplicationDbContext context, ITransaccionInventario trInventario)
        {
            _context = context;
            _trInventario = trInventario;
        }

        // GET: api/Compras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venta>>> GetCompra()
        {
            return await _context.Compra.ToListAsync();
        }

        // GET: api/Compras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Venta>> GetCompra(int id)
        {
            var compra = await _context.Compra.FindAsync(id);

            if (compra == null)
            {
                return NotFound();
            }

            return compra;
        }

        // PUT: api/Compras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompra(int id, Venta compra)
        {
            if (id != compra.ID)
            {
                return BadRequest();
            }

            _context.Entry(compra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompraExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Compras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Venta>> PostCompra(CompraCreate compra)
        {
            List<CompraDetalle> detalles = new List<CompraDetalle>();

            foreach(var detalle in compra.Detalles)
            {
                detalles.Add(new CompraDetalle
                {
                    NoLinea = detalle.NoLinea,
                    ProductoID = detalle.ProductoID,
                    Precio = detalle.Precio,
                    Descripcion = detalle.Descripcion
                });
            }

            Venta c = new Venta
            {
                SocioNegocioID = compra.SocioNegocioID,
                FacturaSerie = compra.FacturaSerie,
                FacturaFecha = compra.FacturaFecha,
                Detalles = detalles
            };
            var respose = await _trInventario.Ingreso(compra);
            //_context.Compra.Add(c);
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompra", new { id = c.ID }, c);
        }

        // DELETE: api/Compras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompra(int id)
        {
            var compra = await _context.Compra.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }

            _context.Compra.Remove(compra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompraExists(int id)
        {
            return _context.Compra.Any(e => e.ID == id);
        }
    }
}