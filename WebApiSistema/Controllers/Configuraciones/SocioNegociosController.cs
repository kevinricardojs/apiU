using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSistema.Data;
using WebApiSistema.DTO.Configuraciones.SocioNegocio;
using WebApiSistema.Models.Configuraciones;

namespace WebApiSistema.Controllers.Configuraciones
{
    [Route("configuraciones/[controller]")]
    [ApiController]
    public class SocioNegociosController : MainController
    {
        private readonly ApplicationDbContext _context;

        public SocioNegociosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SocioNegocios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SocioNegocio>>> GetSocioNegocio()
        {
            return await _context.SocioNegocio.ToListAsync();
        }

        // GET: api/SocioNegocios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SocioNegocio>> GetSocioNegocio(int id)
        {
            var socioNegocio = await _context.SocioNegocio.FindAsync(id);

            if (socioNegocio == null)
            {
                return NotFound();
            }

            return socioNegocio;
        }

        // PUT: api/SocioNegocios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSocioNegocio(int id, SocioNegocio socioNegocio)
        {
            if (id != socioNegocio.ID)
            {
                return BadRequest();
            }

            _context.Entry(socioNegocio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocioNegocioExists(id))
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

        // POST: api/SocioNegocios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SocioNegocio>> PostSocioNegocio(SocioNegocioCreate socioNegocio)
        {
            SocioNegocio s = new SocioNegocio
            {
                Nombre = socioNegocio.Nombre,
                Direccion = socioNegocio.Direccion,
                Nit = socioNegocio.Nit,
                Telefono = socioNegocio.Telefono,
                Tipo = socioNegocio.Tipo,
                SucursalID = GetSucursal()
            };
            _context.SocioNegocio.Add(s);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSocioNegocio", new { id = s.ID }, s);
        }

        // DELETE: api/SocioNegocios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSocioNegocio(int id)
        {
            var socioNegocio = await _context.SocioNegocio.FindAsync(id);
            if (socioNegocio == null)
            {
                return NotFound();
            }

            _context.SocioNegocio.Remove(socioNegocio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SocioNegocioExists(int id)
        {
            return _context.SocioNegocio.Any(e => e.ID == id);
        }
    }
}
