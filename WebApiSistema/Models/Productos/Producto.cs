using System;
using System.ComponentModel.DataAnnotations;
using WebApiSistema.Models.Configuraciones;

namespace WebApiSistema.Models.Productos
{
    public class Producto
    {
        [Key]
        public int ID { get; set; }
        public int ProductoTipoID { get; set; }
        public ProductoTipo ProductoTipo { get; set; }
        public string Descripcion { get; set; }
        public Decimal Precio { get; set; }
        public int SucursalID { get; set; }
        public Sucursal Sucursal { get; set; }
        public int FamiliaProductoID { get; set; }
        public FamiliaProducto FamiliaProducto { get; set; }
    }
}
