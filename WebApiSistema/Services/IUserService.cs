using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiSistema.DTO;
using WebApiSistema.DTO.User;
using WebApiSistema.Models.Usuario;

namespace WebApiSistema.Services
{
    public interface IUserService
    {
        public Task<User> GetUserByEmail(string email);
        public Task<IEnumerable<User>> GetUsers();
        public Task<User> CreateUser(User User, String Password);
        public Task<User> GetUserRoles(User user);
        public Task<User> AddUserRoles(User user, IEnumerable<string> rolesForAdd, IEnumerable<string> rolesForExclude);
        public Task<ResponseUserDTO> VerifyUserCredentiasls(LoginDTO loginInfo);
        public Task<ResponseUserDTO> CreateUser(UserCreate user);
        public Task<ResponseUserDTO> ValidarToken(PeticionTokenDTO token);
    }
}
