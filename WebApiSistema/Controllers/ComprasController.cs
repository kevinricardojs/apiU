using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApiSistema.Data;
using WebApiSistema.DTO.Compras;
using WebApiSistema.Models.Compra;
using WebApiSistema.Services;
using WebApiSistema.Services.Transacciones;

namespace WebApiSistema.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComprasController : MainController
    {
        private readonly ApplicationDbContext _context;
        private readonly ITransaccionInventario _trInventario;
        private readonly IDirectDB _directDB;
        public ComprasController(ApplicationDbContext context, ITransaccionInventario trInventario, IDirectDB directDB)
        {
            _context = context;
            _trInventario = trInventario;
            _directDB = directDB;
        }

        // GET: api/Compras
        [HttpGet]
        public async Task<ActionResult<List<CompraDTO>>> GetCompra()
        {
            int sucursalID = GetSucursal();
            List<CompraDTO> lista = new List<CompraDTO>();
            var list = await _directDB.GetListData($"CALL ComprasRealizadas({sucursalID})");
            if(list.Count > 0)
            {
                string json = JsonConvert.SerializeObject(list);
                lista = JsonConvert.DeserializeObject<List<CompraDTO>>(json);
            }
            return lista;
        }

        // GET: api/Compras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Compra>> GetCompra(int id)
        {
            var compra = await _context.Compra.FindAsync(id);

            if (compra == null)
            {
                return NotFound();
            }

            return compra;
        }

        // PUT: api/Compras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompra(int id, Compra compra)
        {
            if (id != compra.ID)
            {
                return BadRequest();
            }

            _context.Entry(compra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompraExists(id))
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

        // POST: api/Compras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Compra>> PostCompra(CompraCreate compra)
        {
            compra.SucursalID = GetSucursal();
            var response = await _trInventario.Ingreso(compra);

            //_context.Venta.Add(v);
            //await _context.SaveChangesAsync();

            if (response.Success)
            {
                return CreatedAtAction("GetCompra", new { id = response.Compra.ID }, response.Compra);
            }
            else
            {
                return BadRequest(response);
            }
        }

        // DELETE: api/Compras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompra(int id)
        {
            var compra = await _context.Compra.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }

            _context.Compra.Remove(compra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompraExists(int id)
        {
            return _context.Compra.Any(e => e.ID == id);
        }
    }
}
