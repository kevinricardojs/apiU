using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSistema.Data;
using WebApiSistema.DTO.ListaMateriales;
using WebApiSistema.Models.Produccion;

namespace WebApiSistema.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ListaMaterialesController : MainController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public ListaMaterialesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ListaMateriales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListaMateriales>>> GetListaMateriales()
        {
            return await _context.ListaMateriales.ToListAsync();
        }

        // GET: api/ListaMateriales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ListaMateriales>> GetListaMateriales(int id)
        {
            var listaMateriales = await _context.ListaMateriales.FindAsync(id);

            if (listaMateriales == null)
            {
                return NotFound();
            }

            return listaMateriales;
        }

        // PUT: api/ListaMateriales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListaMateriales(int id, ListaMateriales listaMateriales)
        {
            if (id != listaMateriales.ID)
            {
                return BadRequest();
            }

            _context.Entry(listaMateriales).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListaMaterialesExists(id))
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

        // POST: api/ListaMateriales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ListaMaterialesResponse>> PostListaMateriales(ListaMaterialesCreate listaMateriales)
        {
            listaMateriales.SucursalID = GetSucursal();
            var lista = _mapper.Map<ListaMateriales>(listaMateriales);
            _context.ListaMateriales.Add(lista);
            await _context.SaveChangesAsync();

            var listaCreada = _mapper.Map<ListaMaterialesResponse>(lista);
            return CreatedAtAction("GetListaMateriales", new { id = listaCreada.ID }, listaCreada);
        }

        // DELETE: api/ListaMateriales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListaMateriales(int id)
        {
            var listaMateriales = await _context.ListaMateriales.FindAsync(id);
            if (listaMateriales == null)
            {
                return NotFound();
            }

            _context.ListaMateriales.Remove(listaMateriales);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ListaMaterialesExists(int id)
        {
            return _context.ListaMateriales.Any(e => e.ID == id);
        }
    }
}
