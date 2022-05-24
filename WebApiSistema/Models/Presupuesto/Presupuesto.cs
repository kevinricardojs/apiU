using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApiSistema.Models.Configuraciones;

namespace WebApiSistema.Models.Presupuesto
{
    public class Presupuesto
    {
        [Key]
        public int ID { get; set; }
        public int SucursalID { get; set; }
        public Sucursal Sucursal { get; set; }
        public DateTime Creado { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }
        public ICollection<CuentaPresupuesto> Detalles { get; set; }
    }
}
