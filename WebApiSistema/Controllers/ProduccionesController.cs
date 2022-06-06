using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSistema.Data;
using WebApiSistema.DTO.Produccion;
using WebApiSistema.Models.Produccion;
using WebApiSistema.Services.Transacciones;

namespace WebApiSistema.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProduccionesController : MainController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITransaccionInventario _trInventario;

        public ProduccionesController(ApplicationDbContext context, IMapper mapper, ITransaccionInventario trInventario)
        {
            _context = context;
            _mapper = mapper;
            _trInventario = trInventario;
        }

        // GET: api/Produccions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produccion>>> GetProduccion()
        {
            return await _context.Produccion.Where(c => c.SucursalID == GetSucursal()).ToListAsync();
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
        public async Task<ActionResult<ProduccionCreateResponse>> PostProduccion(ProduccionCreate produccion)
        {
            produccion.SucursalID = GetSucursal();
            var response = await _trInventario.Producir(produccion);

            //_context.Venta.Add(v);
            //await _context.SaveChangesAsync();

            if (response.Success)
            {
                return CreatedAtAction("GetProduccion", new { id = response.Produccion.ID }, response.Produccion);
            }
            else
            {
                return BadRequest(response);
            }
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
