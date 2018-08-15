using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Repository.Repositories;

namespace Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly IRepository<User> _repository;

        private readonly IHashMd5Service _hashMd5Service;
        public TokenService(IRepository<User> repository, IHashMd5Service hashMd5Service)
        {
            _repository = repository;
            _hashMd5Service = hashMd5Service;
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

        public async Task<string> GetTokenAsunc(string email, string password)
        {
            if (email == null || password == null)
            {
                throw new ArgumentNullException("Empty username or password.");
            }
            password = _hashMd5Service.GetMd5Hash(password);

            var users = await _repository.GetAllAsync(t => t.Email == email);
            var user = users.First();

            var identity = GetIdentity(user);
            if (identity == null || user.Password != password)
            {
                throw new ArgumentException("Invalid username or password.");
            }

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
            return (JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
    }
}
