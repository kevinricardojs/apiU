using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSistema.Data;
using WebApiSistema.DTO.Presupuesto.CuentasPresupuesto;
using WebApiSistema.Models.Presupuesto;

namespace WebApiSistema.Controllers.Presupuesto
{
    [Route("presupuestos/[controller]")]
    [ApiController]
    public class CuentaPresupuestosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CuentaPresupuestosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CuentaPresupuestos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuentaPresupuesto>>> GetCuentaPresupuesto()
        {
            return await _context.CuentaPresupuesto.ToListAsync();
        }

        // GET: api/CuentaPresupuestos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CuentaPresupuesto>> GetCuentaPresupuesto(int id)
        {
            var cuentaPresupuesto = await _context.CuentaPresupuesto.FindAsync(id);

            if (cuentaPresupuesto == null)
            {
                return NotFound();
            }

            return cuentaPresupuesto;
        }

        // PUT: api/CuentaPresupuestos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuentaPresupuesto(int id, CuentaPresupuestoCreate cuentaPresupuesto)
        {
            CuentaPresupuesto c = new CuentaPresupuesto
            {
                ID = id,
                CuentaID = cuentaPresupuesto.CuentaID,
                SucursalID = cuentaPresupuesto.SucursalID,
                Anio = cuentaPresupuesto.Anio,
                Mes = cuentaPresupuesto.Mes,
                Presupuesto = cuentaPresupuesto.Presupuesto
            };

            _context.Entry(c).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaPresupuestoExists(id))
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

        // POST: api/CuentaPresupuestos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CuentaPresupuesto>> PostCuentaPresupuesto(CuentaPresupuesto cuentaPresupuesto)
        {
            CuentaPresupuesto c = new CuentaPresupuesto
            {
                CuentaID = cuentaPresupuesto.CuentaID,
                SucursalID = cuentaPresupuesto.SucursalID,
                Anio = cuentaPresupuesto.Anio,
                Mes = cuentaPresupuesto.Mes,
                Presupuesto = cuentaPresupuesto.Presupuesto
            };
            _context.CuentaPresupuesto.Add(c);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCuentaPresupuesto", new { id = c.ID }, c);
        }

        // DELETE: api/CuentaPresupuestos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuentaPresupuesto(int id)
        {
            var cuentaPresupuesto = await _context.CuentaPresupuesto.FindAsync(id);
            if (cuentaPresupuesto == null)
            {
                return NotFound();
            }

            _context.CuentaPresupuesto.Remove(cuentaPresupuesto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CuentaPresupuestoExists(int id)
        {
            return _context.CuentaPresupuesto.Any(e => e.ID == id);
        }
    }
}
