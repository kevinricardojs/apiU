using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApiSistema.Models.Configuraciones;

namespace WebApiSistema.Models.Transacciones
{
    public class TransaccionContable
    {
        [Key]
        public int ID { get; set; }
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public int SucursalID { get; set; }
        public Sucursal Sucursal { get; set; }
        public int Tipo { get; set; }
        public int CompraVentaID { get; set; }
        public string Descripcion { get; set; }
        public ICollection<TransaccionDetalleContable> Detalles { get; set; }
    }
}
