using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApiSistema.Data;
using WebApiSistema.DTO.Presupuesto;
using WebApiSistema.DTO.Ventas;
using WebApiSistema.Models.Presupuesto;
using WebApiSistema.Services;

namespace WebApiSistema.Controllers.Presupuestos
{
    [Route("presupuesto/[controller]")]
    [ApiController]
    public class PresupuestosController : MainController
    {
        private readonly ApplicationDbContext _context;
        private readonly IDirectDB _directDB;

        public PresupuestosController(ApplicationDbContext context, IDirectDB directDB)
        {
            _context = context;
            _directDB = directDB;
        }

        // GET: api/Presupuestos
        [HttpGet]
        public async Task<ActionResult<List<Presupuesto>>> GetPresupuesto()
        {
            var listita = await _context.Presupuesto.Include(s => s.Detalles).Where(s => s.SucursalID == GetSucursal()).ToListAsync();
            return listita;
        }

        // GET: api/Presupuestos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Presupuesto>> GetPresupuesto(int id)
        {
            var presupuesto = await _context.Presupuesto.FindAsync(id);

            if (presupuesto == null)
            {
                return NotFound();
            }

            return presupuesto;
        }

        // PUT: api/Presupuestos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPresupuesto(int id, Presupuesto presupuesto)
        {
            var pres =  _context.Presupuesto.AsNoTracking().Include(c => c.Detalles).FirstOrDefault(x => x.ID == id);
            var nuevos = new List<CuentaPresupuesto>();
            if (id != presupuesto.ID)
            {
                return BadRequest();
            }

            _context.Entry(presupuesto).State = EntityState.Modified;
            foreach (var detalle in presupuesto.Detalles)
            {
                var existe = pres.Detalles.Any(x => x.NoLinea == detalle.NoLinea);
                if (existe)
                {
                    _context.Entry(detalle).State = EntityState.Modified;
                }
                else
                {
                    detalle.PresupuestoID = presupuesto.ID;
                    nuevos.Add(detalle);
                }
                
            }
            try
            {
                await _context.SaveChangesAsync();
                if(nuevos.Count > 0)
                {
                    foreach (var d in nuevos)
                    {
                        await _context.CuentaPresupuesto.AddAsync(d);
                    }
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PresupuestoExists(id))
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

        // POST: api/Presupuestos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Presupuesto>> PostPresupuesto(Presupuesto presupuesto)
        {
            presupuesto.SucursalID = GetSucursal();
            presupuesto.Creado = DateTime.Now;
            _context.Presupuesto.Add(presupuesto);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPresupuesto", new { id = presupuesto.ID }, presupuesto);
        }

        // DELETE: api/Presupuestos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePresupuesto(int id)
        {
            var presupuesto = await _context.Presupuesto.FindAsync(id);
            if (presupuesto == null)
            {
                return NotFound();
            }

            _context.Presupuesto.Remove(presupuesto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteLinea/{id}")]
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

        private bool PresupuestoExists(int id)
        {
            return _context.Presupuesto.Any(e => e.ID == id);
        }
    }
}
