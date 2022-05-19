using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSistema.DTO.User
{
    public class UserCreate
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Empresa { get; set; }
        public int Sucursal { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
