
using WebApiSistema.Models.Venta;

namespace WebApiSistema.DTO
{
    public class ResponseDTO
    {
        public bool Success { get; set; }
        public string Mensaje { get; set; } = "";
        public int Linea { get; set; }
        public string Error { get; set; } = "";
    }
}
