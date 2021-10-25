using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AcademyApp.Data.EF.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AcademyApp.API.Authentication
{
    public class JwtAuthManager : IJwtAuthManager
    {
        private readonly IUserRepository _repo;
        private readonly IConfiguration _configuration;

        public JwtAuthManager(IUserRepository repo, IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;
        }
        public async Task<string> Authenticate(string userName, string password)
        {
            var users =await _repo.GetAllAsync();
            if (!users.Any(x => x.UserName == userName && x.Password == password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_configuration.GetSection("AccountKey")["Key"]);
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, userName)
            };
            var claimIdentity = new ClaimsIdentity(claims);
            var signInCreds = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = claimIdentity,
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = signInCreds
            };

            var token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
