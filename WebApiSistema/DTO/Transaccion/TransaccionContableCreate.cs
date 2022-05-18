using System;
using System.Collections.Generic;

namespace WebApiSistema.DTO.Transaccion
{
    public class TransaccionContableCreate
    {
        public int Tipo { get; set; } = 3;
        public string Descripcion { get; set; }
        public ICollection<TransaccionDetalleContableCreate> Detalles { get; set; }
    }
}
