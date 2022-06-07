using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApiSistema.Models.Configuraciones;

namespace WebApiSistema.Models.Servicio
{
    public class Servicio
    {
        [Key]
        public int ID { get; set; }
        public int SocioNegocioID { get; set; }
        [JsonIgnore]
        public SocioNegocio SocioNegocio { get; set; }
        public int SucursalID { get; set; }
        [JsonIgnore]
        public Sucursal Sucursal { get; set; }
        public DateTime FechaCreado { get; set; }
        public string Observaciones { get; set; }
        public ICollection<ServicioDetalle> Detalles { get; set; }
    }
}
