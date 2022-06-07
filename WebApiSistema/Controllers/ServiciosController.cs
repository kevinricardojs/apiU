using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApiSistema.Data;
using WebApiSistema.DTO.Servicio;
using WebApiSistema.DTO.Ventas;
using WebApiSistema.Models.Servicio;
using WebApiSistema.Services;
using WebApiSistema.Services.Transacciones;

namespace WebApiSistema.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiciosController : MainController
    {
        private readonly ApplicationDbContext _context;
        private readonly ITransaccionInventario _trInventario;
        private readonly IDirectDB _directDB;

        public ServiciosController(ApplicationDbContext context,ITransaccionInventario trInventario, IDirectDB directDB)
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
        public async Task<ActionResult<Servicio>> GetServicio(int id)
        {
            var Servicio = await _context.Servicio.FindAsync(id);

            if (Servicio == null)
            {
                return NotFound();
            }

            return Servicio;
        }

        // PUT: api/Servicios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServicio(int id, Servicio Servicio)
        {
            if (id != Servicio.ID)
            {
                return BadRequest();
            }

            _context.Entry(Servicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicioExists(id))
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

        // POST: api/Servicios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Servicio>> PostServicio(ServicioCreate servicio)
        {
            servicio.SucursalID = GetSucursal();
            var response = await _trInventario.Servicio(servicio);

            //_context.Servicio.Add(v);
            //await _context.SaveChangesAsync();

            if (response.Success)
            {
                return CreatedAtAction("GetServicio", new { id = response.Servicio.ID }, response.Servicio);
            }
            else
            {
                return BadRequest(response);
            }
        }

        // DELETE: api/Servicios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServicio(int id)
        {
            var Servicio = await _context.Servicio.FindAsync(id);
            if (Servicio == null)
            {
                return NotFound();
            }

            _context.Servicio.Remove(Servicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServicioExists(int id)
        {
            return _context.Servicio.Any(e => e.ID == id);
        }
    }
}
