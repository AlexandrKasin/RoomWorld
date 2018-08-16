using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Repositories;

namespace Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly IRepository<User> _repository;

        private readonly IHashMd5Service _hashMd5Service;

        private readonly IConfiguration _configuration;
        public TokenService(IRepository<User> repository, IHashMd5Service hashMd5Service, IConfiguration configuration)
        {
            _repository = repository;
            _hashMd5Service = hashMd5Service;
            _configuration = configuration;
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            if (user == null) return null;
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };
            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        public async Task<Token> GetTokenAsunc(string email, string password)
        {
            if (email == null || password == null)
            {
                throw new ArgumentNullException(nameof(email));
            }
            password = _hashMd5Service.GetMd5Hash(password);

            var user = await (await _repository.GetAllAsync(t => t.Email == email)).FirstOrDefaultAsync();
            
            var identity = GetIdentity(user);
            if (identity == null || user.Password != password)
            {
                throw new ArgumentException("Invalid username or password.");
            }

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: _configuration["AuthOption:Issuer"],
                audience: _configuration["AuthOption:Audience"],
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(Convert.ToInt32(_configuration["AuthOption:Lifetime"]))),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["AuthOption:Key"])), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            
            var response = new Token
            {
                AccessToken = encodedJwt,
                Username = identity.Name
            };
            return response;
        }
    }
}
