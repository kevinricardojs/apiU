using System;
using System.Collections.Generic;

namespace WebApiSistema.DTO.Transaccion
{
    public class TransaccionContableResponse : TransaccionContableCreate
    {
        public int ID { get; set; }

        public List<TransaccionDetalleContableResponse> Detalles { get; set; }
    }
}
