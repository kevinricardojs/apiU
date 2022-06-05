using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSistema.Data;
using WebApiSistema.DTO.Productos.ProductoTipo;
using WebApiSistema.Models.Productos;

namespace WebApiSistema.Controllers.Productos
{
    [Route("productos/[controller]")]
    [ApiController]
    public class ProductoTiposController : MainController
    {
        private readonly ApplicationDbContext _context;

        public ProductoTiposController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductoTipos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoTipo>>> GetProductoTipo()
        {
            return await _context.ProductoTipo.Where(c => c.SucursalID == GetSucursal()).ToListAsync();
        }

        // GET: api/ProductoTipos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoTipo>> GetProductoTipo(int id)
        {
            var productoTipo = await _context.ProductoTipo.FindAsync(id);

            if (productoTipo == null)
            {
                return NotFound();
            }

            return productoTipo;
        }

        // PUT: api/ProductoTipos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductoTipo(int id, ProductoTipoCreate productoTipo)
        {
            ProductoTipo p = new ProductoTipo
            {
                ID = id,
                Descripcion = productoTipo.Descripcion,
                SucursalID = GetSucursal()
            };

            _context.Entry(p).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoTipoExists(id))
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

        // POST: api/ProductoTipos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductoTipo>> PostProductoTipo(ProductoTipoCreate productoTipo)
        {
            
            ProductoTipo p = new ProductoTipo
            {
                Descripcion = productoTipo.Descripcion
            };
            p.SucursalID = GetSucursal();
            _context.ProductoTipo.Add(p);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductoTipo", new { id = p.ID }, p);
        }

        // DELETE: api/ProductoTipos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductoTipo(int id)
        {
            var productoTipo = await _context.ProductoTipo.FindAsync(id);
            if (productoTipo == null)
            {
                return NotFound();
            }

            _context.ProductoTipo.Remove(productoTipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoTipoExists(int id)
        {
            return _context.ProductoTipo.Any(e => e.ID == id);
        }
    }
}
