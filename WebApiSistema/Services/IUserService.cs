using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
