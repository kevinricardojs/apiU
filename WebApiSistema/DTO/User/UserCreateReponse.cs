
namespace WebApiSistema.DTO.User
{
    public class UserCreateReponse
    {
        public int ID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Empresa { get; set; }
        public int Sucursal { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int IsAdmin { get; set; }
    }
}
