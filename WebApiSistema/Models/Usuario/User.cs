using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WebApiSistema.Models.Usuario
{
    public class User : IdentityUser<int>
    {

        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Status { get; set; } = 1;
        public int Empresa { get; set; }
        public int Sucursal { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        [NotMapped]
        public virtual ICollection<String> RoleNames { get; set; }

        [NotMapped]
        public String token { get; set; }

        [NotMapped]
        public virtual ICollection<Role> Roles { get; set; }
        [NotMapped]
        public virtual ICollection<Role> UnAssignedRoles { get; set; }
    }
    }
