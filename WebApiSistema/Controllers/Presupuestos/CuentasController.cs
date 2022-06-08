using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSistema.Data;
using WebApiSistema.DTO.Presupuesto.Cuentas;
using WebApiSistema.Models.Presupuesto;

namespace WebApiSistema.Controllers.Presupuestos
{
    [Route("presupuesto/[controller]")]
    [ApiController]
    public class CuentasController : MainController
    {
        private readonly ApplicationDbContext _context;

        public CuentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cuentas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuenta>>> GetCuenta()
        {
            return await _context.Cuenta.ToListAsync();
        }

        // GET: api/Cuentas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cuenta>> GetCuenta(int id)
        {
            var cuenta = await _context.Cuenta.FindAsync(id);

            if (cuenta == null)
            {
                return NotFound();
            }

            return cuenta;
        }

        // PUT: api/Cuentas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuenta(int id, CuentaCreate cuenta)
        {
            Cuenta c = new Cuenta
            {
                ID = id,
                CodigoCuenta = cuenta.CodigoCuenta,
                Descripcion = cuenta.Descripcion,
                Nivel = cuenta.Nivel
            };

            _context.Entry(c).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaExists(id))
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

        // POST: api/Cuentas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cuenta>> PostCuenta(CuentaCreate cuenta)
        {
            Cuenta c = new Cuenta
            {
                CodigoCuenta = cuenta.CodigoCuenta,
                Descripcion = cuenta.Descripcion,
                Nivel = cuenta.Nivel
            };
            _context.Cuenta.Add(c);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCuenta", new { id = c.ID }, c);
        }

        // DELETE: api/Cuentas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuenta(int id)
        {
            var cuenta = await _context.Cuenta.FindAsync(id);
            if (cuenta == null)
            {
                return NotFound();
            }

            _context.Cuenta.Remove(cuenta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CuentaExists(int id)
        {
            return _context.Cuenta.Any(e => e.ID == id);
        }
    }
}
