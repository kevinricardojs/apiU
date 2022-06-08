using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApiSistema.Models.Configuraciones;

namespace WebApiSistema.Models.Presupuesto
{
    public class CuentaPresupuesto
    {
        [Key]
        public int ID { get; set; }
        public int CuentaID { get; set; }
        [JsonIgnore]
        public Cuenta Cuenta { get; set; }
        public Decimal Presupuesto { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public int NoLinea { get; set; }
        public int PresupuestoID { get; set; }
        [JsonIgnore]
        public Presupuesto PresupuestoT { get; set; }
    }
}
