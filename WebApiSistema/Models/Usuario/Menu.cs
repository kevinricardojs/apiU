using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSistema.Models.Usuario
{
    public class Menu
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public virtual ICollection<RoleMenu> RoleMenus { get; set; }

    }
}
