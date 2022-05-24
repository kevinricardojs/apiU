using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSistema.Data;
using WebApiSistema.DTO.Productos.FamiliaProducto;
using WebApiSistema.Models.Productos;

namespace WebApiSistema.Controllers.Productos
{
    [Route("productos/[controller]")]
    [ApiController]
    public class FamiliaProductosController : MainController
    {
        private readonly ApplicationDbContext _context;

        public FamiliaProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FamiliaProductos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FamiliaProducto>>> GetFamiliaProducto()
        {
            return await _context.FamiliaProducto.ToListAsync();
        }

        // GET: api/FamiliaProductos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FamiliaProducto>> GetFamiliaProducto(int id)
        {
            var familiaProducto = await _context.FamiliaProducto.FindAsync(id);

            if (familiaProducto == null)
            {
                return NotFound();
            }

            return familiaProducto;
        }

        // PUT: api/FamiliaProductos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFamiliaProducto(int id, FamiliaProductoCreate familiaProducto)
        {
            FamiliaProducto f = new FamiliaProducto
            {
                ID = id,
                Descripcion = familiaProducto.Descripcion,
                CuentaID = familiaProducto.CuentaID,
                CuentaIDO = familiaProducto.CuentaIDO
            };

            _context.Entry(f).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FamiliaProductoExists(id))
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

        // POST: api/FamiliaProductos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FamiliaProducto>> PostFamiliaProducto(FamiliaProductoCreate familiaProducto)
        {
            FamiliaProducto f = new FamiliaProducto
            {
                Descripcion = familiaProducto.Descripcion,
                CuentaID = familiaProducto.CuentaID,
                CuentaIDO = familiaProducto.CuentaIDO
            };
            _context.FamiliaProducto.Add(f);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFamiliaProducto", new { id = f.ID }, f);
        }

        // DELETE: api/FamiliaProductos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFamiliaProducto(int id)
        {
            var familiaProducto = await _context.FamiliaProducto.FindAsync(id);
            if (familiaProducto == null)
            {
                return NotFound();
            }

            _context.FamiliaProducto.Remove(familiaProducto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FamiliaProductoExists(int id)
        {
            return _context.FamiliaProducto.Any(e => e.ID == id);
        }
    }
}
