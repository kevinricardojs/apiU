using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSistema.DTO.Productos.Producto
{
    public class ProductoCreate
    {
        public int ProductoTipoID { get; set; }
        public string Descripcion { get; set; }
        public Decimal Precio { get; set; }
        public int SucursalID { get; set; }
        public int FamiliaProductoID { get; set; }
    }
}
