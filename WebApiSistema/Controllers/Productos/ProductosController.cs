using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSistema.Data;
using WebApiSistema.DTO.Productos.Producto;
using WebApiSistema.Models.Productos;

namespace WebApiSistema.Controllers.Productos
{
    [Route("productos/[controller]")]
    [ApiController]
    public class ProductosController : MainController
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProducto()
        {
            return await _context.Producto.Where(c => c.SucursalID == GetSucursal()).ToListAsync();
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Producto.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // PUT: api/Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, ProductoCreate producto)
        {
            Producto p = new Producto
            {
                ID = id,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                ProductoTipoID = producto.ProductoTipoID,
                FamiliaProductoID = producto.FamiliaProductoID
            };
            p.SucursalID = GetSucursal();

            _context.Entry(p).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
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

        // POST: api/Productos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(ProductoCreate producto)
        {
            
            Producto p = new Producto
            {
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                ProductoTipoID = producto.ProductoTipoID,
                FamiliaProductoID = producto.FamiliaProductoID
            };
            p.SucursalID = GetSucursal();
            _context.Producto.Add(p);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new { id = p.ID }, p);
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoExists(int id)
        {
            return _context.Producto.Any(e => e.ID == id);
        }
    }
}
