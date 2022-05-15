using System;
using System.ComponentModel.DataAnnotations;
using WebApiSistema.Models.Configuraciones;
using WebApiSistema.Models.Productos;

namespace WebApiSistema.Models.Transacciones
{
    public class TransaccionDetalleInventario
    {
        [Key]
        public int ID { get; set; }
        public int ProductoID { get; set; }
        public Producto Producto { get; set; }
        public int SucursalID { get; set; }
        public Sucursal Sucursal { get; set; }
        public int Linea { get; set; }
        public Decimal Valor { get; set; }
        public Decimal Entrada { get; set; }
        public Decimal Salida { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
