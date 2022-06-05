using System;

namespace WebApiSistema.DTO.Compras
{
    public class CompraDTO
    {
        public int ID { get; set; }
        public DateTime Creado { get; set; }
        public string Proveedor { get; set; }
        public string FacturaSerie { get; set; }
        public Decimal Cantidad { get; set; }
        public Decimal Total { get; set; }
    }
}
