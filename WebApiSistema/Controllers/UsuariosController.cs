using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSistema.Data;
using WebApiSistema.Models.Usuario;

namespace WebApiSistema.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : MainController
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsuario()
        {
            return await _context.Users.ToListAsync();
        }

       
    }
}
