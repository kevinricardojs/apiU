using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSistema.Data;
using WebApiSistema.Models.Usuario;

namespace WebApiSistema.Controllers.Configuraciones
{
    [Route("configuraciones/[controller]")]
    [ApiController]
    public class UsuarioSucursalController : MainController
    {
        private readonly ApplicationDbContext _context;

        public UsuarioSucursalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UsuarioSucursal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioSucursal>>> GetUsuarioSucursal()
        {
            return await _context.UsuarioSucursal.ToListAsync();
        }

        // GET: api/UsuarioSucursal/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioSucursal>> GetUsuarioSucursal(int id)
        {
            var usuarioSucursal = await _context.UsuarioSucursal.FindAsync(id);

            if (usuarioSucursal == null)
            {
                return NotFound();
            }

            return usuarioSucursal;
        }

        // PUT: api/UsuarioSucursal/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpresa(int id, UsuarioSucursal usuarioSucursal)
        {
            if (id != usuarioSucursal.ID)
            {
                return BadRequest();
            }

            _context.Entry(usuarioSucursal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioSucursalExists(id))
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

        // POST: api/UsuarioSucursal
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsuarioSucursal>> PostEmpresa(UsuarioSucursal usuarioSucursal)
        {
            _context.UsuarioSucursal.Add(usuarioSucursal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuarioSucursal", new { id = usuarioSucursal.ID }, usuarioSucursal);
        }

        // DELETE: api/UsuarioSucursal/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioSucursal(int id)
        {
            var empresa = await _context.UsuarioSucursal.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }

            _context.UsuarioSucursal.Remove(empresa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioSucursalExists(int id)
        {
            return _context.UsuarioSucursal.Any(e => e.ID == id);
        }
    }
}
