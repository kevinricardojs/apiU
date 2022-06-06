using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApiSistema.Data;
using WebApiSistema.DTO.Salidas;
using WebApiSistema.Models.Salida;
using WebApiSistema.Services;
using WebApiSistema.Services.Transacciones;

namespace WebApiSistema.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SalidasController : MainController
    {
        private readonly ApplicationDbContext _context;
        private readonly ITransaccionInventario _trInventario;
        private readonly IDirectDB _directDB;

        public SalidasController(ApplicationDbContext context,ITransaccionInventario trInventario, IDirectDB directDB)
        {
            _context = context;
            _trInventario = trInventario;
            _directDB = directDB;
        }

        // GET: api/Salidas
        [HttpGet]
        public async Task<ActionResult<List<SalidaDTO>>> GetSalida()
        {
            int sucursalID = GetSucursal();
            List<SalidaDTO> lista = new List<SalidaDTO>();
            var list = await _directDB.GetListData($"CALL SalidasRealizadas({sucursalID})");
            if (list.Count > 0)
            {
                string json = JsonConvert.SerializeObject(list);
                lista = JsonConvert.DeserializeObject<List<SalidaDTO>>(json);
            }
            return lista;
        }

        // GET: api/Salidas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Salida>> GetSalida(int id)
        {
            var Salida = await _context.Salida.FindAsync(id);

            if (Salida == null)
            {
                return NotFound();
            }

            return Salida;
        }

        // PUT: api/Salidas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalida(int id, Salida Salida)
        {
            if (id != Salida.ID)
            {
                return BadRequest();
            }

            _context.Entry(Salida).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalidaExists(id))
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

        // POST: api/Salidas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Salida>> PostSalida(SalidaCreate salida)
        {
            salida.SucursalID = GetSucursal();
            var response = await _trInventario.SalidaStock(salida);

            //_context.Salida.Add(v);
            //await _context.SaveChangesAsync();

            if (response.Success)
            {
                return CreatedAtAction("GetSalida", new { id = response.Salida.ID }, response.Salida);
            }
            else
            {
                return BadRequest(response);
            }
        }

        // DELETE: api/Salidas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalida(int id)
        {
            var Salida = await _context.Salida.FindAsync(id);
            if (Salida == null)
            {
                return NotFound();
            }

            _context.Salida.Remove(Salida);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalidaExists(int id)
        {
            return _context.Salida.Any(e => e.ID == id);
        }
    }
}
