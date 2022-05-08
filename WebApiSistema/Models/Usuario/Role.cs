using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiSistema.Models.Usuario
{
    public class Role : IdentityRole<int>
    {
        public int Status { get; set; } = 1;
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RoleMenu> RoleMenus { get; set; }
        [NotMapped]
        public virtual ICollection<Menu> Menus { get; set; }
        [NotMapped]
        public virtual ICollection<Menu> UnAssignedMenus { get; set; }

    }
}
