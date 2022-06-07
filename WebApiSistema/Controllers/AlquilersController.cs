using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApiSistema.Data;
using WebApiSistema.DTO.Alquiler;
using WebApiSistema.DTO.Ventas;
using WebApiSistema.Models.Alquiler;
using WebApiSistema.Models.Venta;
using WebApiSistema.Services;
using WebApiSistema.Services.Transacciones;

namespace WebApiSistema.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlquilersController : MainController
    {
        private readonly ApplicationDbContext _context;
        private readonly ITransaccionInventario _trInventario;
        private readonly IDirectDB _directDB;

        public AlquilersController(ApplicationDbContext context,ITransaccionInventario trInventario, IDirectDB directDB)
        {
            _context = context;
            _trInventario = trInventario;
            _directDB = directDB;
        }

        // GET: api/Ventas
        [HttpGet]
        public async Task<ActionResult<List<VentaDTO>>> GetVenta()
        {
            int sucursalID = GetSucursal();
            List<VentaDTO> lista = new List<VentaDTO>();
            var list = await _directDB.GetListData($"CALL VentasRealizadas({sucursalID})");
            if (list.Count > 0)
            {
                string json = JsonConvert.SerializeObject(list);
                lista = JsonConvert.DeserializeObject<List<VentaDTO>>(json);
            }
            return lista;
        }

        // GET: api/Ventas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alquiler>> GetAlquiler(int id)
        {
            var Alquiler = await _context.Alquiler.FindAsync(id);

            if (Alquiler == null)
            {
                return NotFound();
            }

            return Alquiler;
        }

        // PUT: api/Alquilers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlquiler(int id, Alquiler Alquiler)
        {
            if (id != Alquiler.ID)
            {
                return BadRequest();
            }

            _context.Entry(Alquiler).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlquilerExists(id))
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

        // POST: api/Alquilers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Alquiler>> PostAlquiler(AlquilerCreate alquiler)
        {
            alquiler.SucursalID = GetSucursal();
            var response = await _trInventario.Alquiler(alquiler);

            //_context.Alquiler.Add(v);
            //await _context.SaveChangesAsync();

            if (response.Success)
            {
                return CreatedAtAction("GetAlquiler", new { id = response.Alquiler.ID }, response.Alquiler);
            }
            else
            {
                return BadRequest(response);
            }
        }

        // DELETE: api/Alquilers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlquiler(int id)
        {
            var Alquiler = await _context.Alquiler.FindAsync(id);
            if (Alquiler == null)
            {
                return NotFound();
            }

            _context.Alquiler.Remove(Alquiler);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlquilerExists(int id)
        {
            return _context.Alquiler.Any(e => e.ID == id);
        }
    }
}
