using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Data.Entity.UserEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Repositories;
using Service.DTO;
using Service.DTO.UserDTO;
using Service.Exceptions;

namespace Service.Services.UserServices
{
    public class TokenService : ITokenService
    {
        private readonly IRepository<User> _repository;
        private readonly IHashMd5 _hashMd5Service;
        private readonly IConfiguration _configuration;

        public TokenService(IRepository<User> repository, IHashMd5 hashMd5Service, IConfiguration configuration)
        {
            _repository = repository;
            _hashMd5Service = hashMd5Service;
            _configuration = configuration;
        }

        private static ClaimsIdentity GetIdentity(User user)
        {
            if (user == null) return null;
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            claims.AddRange(user.UserRoles
                .Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Role.Name)).ToList());
            var claimsIdentity =
                new ClaimsIdentity(claims, "TokenViewModel", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        public async Task<TokenDTO> GetTokenAsync(AuthorizeDTO authorize)
        {
            authorize.Password = _hashMd5Service.GetMd5Hash(authorize.Password);
            var user = await (await _repository.GetAllAsync(t =>
                t.Email.ToUpper().Equals(authorize.Email))).Include("UserRoles.Role").FirstOrDefaultAsync();
            if (user == null)
                throw new EntityNotExistException("Email address does not exist.");

            var identity = GetIdentity(user);
            if (user.Password != authorize.Password || identity == null)
                throw new IncorrectParamsException("Incorrect email or password.");

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: _configuration["AuthOption:Issuer"],
                audience: _configuration["AuthOption:Audience"],
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(Convert.ToInt32(_configuration["AuthOption:Lifetime"]))),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["AuthOption:Key"])),
                    SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new TokenDTO
            {
                AccessToken = encodedJwt,
                Username = identity.Name,
                Roles = user.UserRoles.Select(role => role.Role.Name).ToList()
            };
            return response;
        }
    }
}