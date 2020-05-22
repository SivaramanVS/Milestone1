using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using BusinessService.Data.DBModel;
using BusinessService.Domain.Helpers;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Linq;
using System.Text;

namespace BusinessService.Domain.Services
{
    public class UserService : IUserService
    {
        /// <param name="Authentication"></param>
        #region user
        private readonly List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Admin", LastName = "User", Username = "admin", Password = "admin", Role = Role.Admin },
            new User { Id = 2, FirstName = "Normal", LastName = "User", Username = "user", Password = "user", Role = Role.User },
            new User { Id = 3, FirstName = "Admin", LastName = "User", Username = "Rajesh", Password = "raja123", Role = Role.Admin },
            new User { Id = 4, FirstName = "Normal", LastName = "User", Username = "Viswanath", Password = "vis123", Role = Role.User },
            new User { Id = 5, FirstName = "Admin", LastName = "User", Username = "Sivaraman", Password = "siva123", Role = Role.Admin }
        };
        #endregion        
        /// <param name="Authorization"></param>        
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public IEnumerable<User> GetAll()
        {
            return _users.WithoutPasswords();
        }

        public User GetById(int id)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);
            return user.WithoutPassword();
        }
    }
}
