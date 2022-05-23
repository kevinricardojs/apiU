

namespace WebApiSistema.DTO.User
{
    public class ResponseUserDTO : ResponseDTO
    {
        public UserCreateReponse user { get; set; }
        public string Token { get; set; }
    }
}
