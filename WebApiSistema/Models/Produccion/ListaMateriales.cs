using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApiSistema.Models.Configuraciones;
using WebApiSistema.Models.Productos;

namespace WebApiSistema.Models.Produccion
{
    public class ListaMateriales
    {
        [Key]
        public int ID { get; set; }
        public int ProductoID { get; set; }
        public int SucursalID { get; set; }
        [JsonIgnore]
        public Sucursal Sucursal { get; set; }
        [JsonIgnore]
        public Producto Producto { get; set; }
        public DateTime Creado { get; set; }
        public string Instrucciones { get; set; }
        public decimal Cantidad { get; set; }
        public ICollection<Materiales> Materiales { get; set; }
    }
}
