using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApiSistema.Data;
using WebApiSistema.DTO.Ventas;
using WebApiSistema.Models.Venta;
using WebApiSistema.Services;
using WebApiSistema.Services.Transacciones;

namespace WebApiSistema.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VentasController : MainController
    {
        private readonly ApplicationDbContext _context;
        private readonly ITransaccionInventario _trInventario;
        private readonly IDirectDB _directDB;

        public VentasController(ApplicationDbContext context,ITransaccionInventario trInventario, IDirectDB directDB)
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
        public async Task<ActionResult<Venta>> GetVenta(int id)
        {
            var venta = await _context.Venta.FindAsync(id);

            if (venta == null)
            {
                return NotFound();
            }

            return venta;
        }

        // PUT: api/Ventas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenta(int id, Venta venta)
        {
            if (id != venta.ID)
            {
                return BadRequest();
            }

            _context.Entry(venta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentaExists(id))
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

        // POST: api/Ventas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Venta>> PostVenta(VentaCreate venta)
        {
            venta.SucursalID = GetSucursal();
            var response = await _trInventario.Egreso(venta);

            //_context.Venta.Add(v);
            //await _context.SaveChangesAsync();

            if (response.Success)
            {
                return CreatedAtAction("GetVenta", new { id = response.Venta.ID }, response.Venta);
            }
            else
            {
                return BadRequest(response);
            }
        }

        // DELETE: api/Ventas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenta(int id)
        {
            var venta = await _context.Venta.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }

            _context.Venta.Remove(venta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VentaExists(int id)
        {
            return _context.Venta.Any(e => e.ID == id);
        }
    }
}
