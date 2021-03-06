using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSistema.Data;
using WebApiSistema.DTO.Configuraciones.Sucursal;
using WebApiSistema.Models.Configuraciones;

namespace WebApiSistema.Controllers.Configuraciones
{
    [Route("configuraciones/[controller]")]
    [ApiController]
    public class SucursalesController : MainController
    {
        private readonly ApplicationDbContext _context;

        public SucursalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Sucursales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sucursal>>> GetASucursal()
        {
            return await _context.Sucursal.ToListAsync();
        }

        // GET: api/Sucursales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sucursal>> GetSucursal(int id)
        {
            var sucursal = await _context.Sucursal.FindAsync(id);

            if (sucursal == null)
            {
                return NotFound();
            }

            return sucursal;
        }

        // PUT: api/Sucursales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSucursal(int id, SucursalCreate sucursal)
        {
            Sucursal s = new Sucursal
            {
                ID = id,
                Descripcion = sucursal.Descripcion,
                Direccion = sucursal.Direccion,
                EmpresaID = sucursal.EmpresaID
            };

            _context.Entry(s).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SucursalExists(id))
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

        // POST: api/Sucursales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sucursal>> PostSucursal(SucursalCreate sucursal)
        {
            Sucursal s = new Sucursal
            {
                Descripcion = sucursal.Descripcion,
                EmpresaID = sucursal.EmpresaID,
                Direccion = sucursal.Direccion
            };
            _context.Sucursal.Add(s);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSucursal", new { id = s.ID }, s);
        }

        // DELETE: api/Sucursales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSucursal(int id)
        {
            var sucursal = await _context.Sucursal.FindAsync(id);
            if (sucursal == null)
            {
                return NotFound();
            }

            _context.Sucursal.Remove(sucursal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SucursalExists(int id)
        {
            return _context.Sucursal.Any(e => e.ID == id);
        }
    }
}
