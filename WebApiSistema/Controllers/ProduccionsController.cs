using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSistema.Data;
using WebApiSistema.Models.Produccion;

namespace WebApiSistema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduccionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProduccionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Produccions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produccion>>> GetProduccion()
        {
            return await _context.Produccion.ToListAsync();
        }

        // GET: api/Produccions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produccion>> GetProduccion(int id)
        {
            var produccion = await _context.Produccion.FindAsync(id);

            if (produccion == null)
            {
                return NotFound();
            }

            return produccion;
        }

        // PUT: api/Produccions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduccion(int id, Produccion produccion)
        {
            if (id != produccion.ID)
            {
                return BadRequest();
            }

            _context.Entry(produccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProduccionExists(id))
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

        // POST: api/Produccions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produccion>> PostProduccion(Produccion produccion)
        {
            _context.Produccion.Add(produccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduccion", new { id = produccion.ID }, produccion);
        }

        // DELETE: api/Produccions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduccion(int id)
        {
            var produccion = await _context.Produccion.FindAsync(id);
            if (produccion == null)
            {
                return NotFound();
            }

            _context.Produccion.Remove(produccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProduccionExists(int id)
        {
            return _context.Produccion.Any(e => e.ID == id);
        }
    }
}
