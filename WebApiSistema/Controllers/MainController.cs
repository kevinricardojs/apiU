using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace WebApiSistema.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MainController : ControllerBase
    {
        protected int GetSucursal()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            int SucursalID = Int32.Parse(identity.FindFirst("SucursalID").Value);
            return SucursalID;
        }
    }
}
