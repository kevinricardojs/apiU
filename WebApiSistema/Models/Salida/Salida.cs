using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiSistema.Models.Salida
{
    public class Salida
    {
        [Key]
        public int ID { get; set; }
        public string Comentarios { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
