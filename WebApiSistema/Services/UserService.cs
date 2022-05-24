using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;
using System.Transactions;
using WebApiSistema.Data;
using WebApiSistema.Models.Usuario;
using WebApiSistema.DTO.User;
using AutoMapper;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace WebApiSistema.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly string _tokenEncryptor;
        public UserService(UserManager<User> userManager, ApplicationDbContext context, IMapper mapper, IConfiguration config)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _tokenEncryptor = config.GetSection("AppSettings:Token").Value;
        }

        public async Task<ResponseUserDTO> VerifyUserCredentiasls(LoginDTO loginInfo)
        {
            var user = await GetUserByEmail(loginInfo.email);
            var result = await _userManager.CheckPasswordAsync(user, loginInfo.password);
            if (result)
            {
                string token = GenerateJwtToken(user, loginInfo.SucursalID);
                var usuario= _mapper.Map<UserCreateReponse>(user);
                return new ResponseUserDTO
                {
                    Success = true,
                    Mensaje = "Inicio de sesión correcto",
                    user = usuario,
                    Token = token
                };
            }
            return new ResponseUserDTO
            {
                Success = false,
                Error = "Usuario o password incorrecto, verifique su información"
            };
        }

        public async Task<ResponseUserDTO> CreateUser(UserCreate user)
        {
            User u = _mapper.Map<User>(user);
            var result = await _userManager.CreateAsync(u, user.Password);

            if (result.Succeeded)
            {
                var usuarioCreado = _mapper.Map<UserCreateReponse>(u);
                return new ResponseUserDTO
                {
                    Success = true,
                    Mensaje = "Usuario Creado Correctamente",
                    user = usuarioCreado
                };
            }

            List<IdentityError> errorList = result.Errors.ToList();
            var errors = string.Join(", ", errorList.Select(e => e.Description));
            return new ResponseUserDTO {
                Success = false,
                Error = errors
            };
        }
        public async Task<User> GetUserByEmail(string email)
        {
            var userFrom = await _context.Users
            .FirstOrDefaultAsync(user => user.Email == email);

            return userFrom;

        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            //var roles = _userManager.GetRolesAsync()
            var users = await _userManager.Users.AsNoTracking().ToListAsync();

            return users;
        }
        public async Task<User> CreateUser(User User, String Password)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await _userManager.CreateAsync(User, Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRolesAsync(User, User.RoleNames);
                }
                scope.Complete();
            }
            return User;
        }
        public async Task<User> GetUserRoles(User user)
        {
            user.RoleNames = await _userManager.GetRolesAsync(user);
            return user;
        }

        public async Task<User> AddUserRoles(User user, IEnumerable<string> rolesForAdd, IEnumerable<string> rolesForExclude)
        {
            await _userManager.AddToRolesAsync(user, rolesForAdd.Except(rolesForExclude));
            return user;
        }
        public async Task<User> RemoveUserRoles(User user, IEnumerable<string> rolesForRemove, IEnumerable<string> rolesForExclude)
        {
            await _userManager.RemoveFromRolesAsync(user, rolesForRemove.Except(rolesForExclude));
            return user;
        }
        public async Task<User> UpdateUser(User userForBeUpdated)
        {
            IdentityResult result = await _userManager.UpdateAsync(userForBeUpdated);

            //var userFromRepo = await GetUserById(userForBeUpdated.Id);

            return userForBeUpdated;
        }
        public async Task<User> GetUserByUserName(string userName)
        {
            var userFrom = await _context.Users.AsNoTracking()
            .Select(user => new User
            {
                Id = user.Id,
                UserName = user.UserName,
                Nombres = user.Nombres,
                Apellidos = user.Apellidos,
                SecurityStamp = user.SecurityStamp,
                PasswordHash = user.PasswordHash,
                Status = user.Status
            })
            .FirstOrDefaultAsync(user => user.UserName == userName);

            return userFrom;

        }
        public async Task<User> ResetPassword(User user, string newPassword)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            return user;
        }

        private string GenerateJwtToken(User user, int SucursalID)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("SucursalID", SucursalID.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenEncryptor));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
