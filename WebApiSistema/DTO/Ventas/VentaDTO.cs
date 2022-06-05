using System;

namespace WebApiSistema.DTO.Ventas
{
    public class VentaDTO
    {
        public int ID { get; set; }
        public DateTime Creado { get; set; }
        public string Cliente { get; set; }
        public string FacturaSerie { get; set; }
        public Decimal Cantidad { get; set; }
        public Decimal Total { get; set; }
    }
}
