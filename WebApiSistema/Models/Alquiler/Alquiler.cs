using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebApiSistema.Models.Configuraciones;

namespace WebApiSistema.Models.Alquiler
{
    public class Alquiler
    {
        [Key]
        public int ID { get; set; }
        public int SocioNegocioID { get; set; }
        [JsonIgnore]
        public SocioNegocio SocioNegocio { get; set; }
        public int SucursalID { get; set; }
        [JsonIgnore]
        public Sucursal Sucursal { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public DateTime FechaCreado { get; set; }
        public string Observaciones { get; set; }
        public ICollection<AlquilerDetalle> Detalles { get; set; }
    }
}
