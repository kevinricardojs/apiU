using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApiSistema.Services;
using WebApiSistema.DTO.User;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using WebApiSistema.Models.Configuraciones;
using Newtonsoft.Json;
using WebApiSistema.DTO;

namespace WebApiSistema.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IDirectDB _directDB;

        public AutenticacionController(IUserService userService, IDirectDB directDB)
        {
            _userService = userService;
            _directDB = directDB;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginInfo)
        {
            var result = await _userService.VerifyUserCredentiasls(loginInfo);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost]
        [Route("CreateUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateUser([FromBody] UserCreate user)
        {
            var result = await _userService.CreateUser(user);
            if (result.Success)
            {
                return Ok(result.user);
            }

            return BadRequest(result);
        }

        [HttpGet]
        [Route("Sucursales")]
        public async Task<List<Sucursal>> GetSucursales(string email)
        {
            List<Sucursal> lista = new List<Sucursal>();
            var l = await _directDB.GetListData($"CALL SucursalUsuario('{email}')");
            if (l.Count > 0)
            {
                string json = JsonConvert.SerializeObject(l);
                lista = JsonConvert.DeserializeObject<List<Sucursal>>(json);
            }
            return lista;
        }

        [HttpPost]
        [Route("ValidarToken")]
        public async Task<ActionResult<ResponseUserDTO>> ValidarToken([FromBody] PeticionTokenDTO peticion)
        {
            var resultado = await _userService.ValidarToken(peticion);
            if (resultado.Success == true)
            {
                return Ok(resultado);
            }

            return BadRequest(resultado);
        }
    }
}
