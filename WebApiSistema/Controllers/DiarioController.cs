using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSistema.Data;
using WebApiSistema.DTO.Transaccion;
using WebApiSistema.Models.Transacciones;

namespace WebApiSistema.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DiarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DiarioController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransaccionContable>>> GetTransaccionContable()
        {
            return await _context.TransaccionContable.ToListAsync();
        }

        // GET: api/TransaccionContables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransaccionContable>> GetTransaccionContable(int id)
        {
            var TransaccionContable = await _context.TransaccionContable.FindAsync(id);

            if (TransaccionContable == null)
            {
                return NotFound();
            }

            return TransaccionContable;
        }

        // PUT: api/TransaccionContables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaccionContable(int id, TransaccionContable transaccionContable)
        {
            


            _context.Entry(transaccionContable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransaccionContableExists(id))
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

        // POST: api/TransaccionContables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TransaccionContable>> PostTransaccionContable(TransaccionContableCreate transaccionContable)
        {
            var t = _mapper.Map<TransaccionContable>(transaccionContable);
            _context.TransaccionContable.Add(t);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<TransaccionContableResponse>(t);
            return CreatedAtAction("GetTransaccionContable", new { id = response.ID }, response);
        }

        // DELETE: api/TransaccionContables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaccionContable(int id)
        {
            var TransaccionContable = await _context.TransaccionContable.FindAsync(id);
            if (TransaccionContable == null)
            {
                return NotFound();
            }

            _context.TransaccionContable.Remove(TransaccionContable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransaccionContableExists(int id)
        {
            return _context.TransaccionContable.Any(e => e.ID == id);
        }
    }
}
