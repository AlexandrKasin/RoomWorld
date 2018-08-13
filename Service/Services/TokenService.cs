using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RoomWorld;
using RoomWorld.Services;
using Service.iServices;

namespace Service
{
    public class TokenService : ITokenService
    {
        private IUserService userService;
        public TokenService(IUserService userService)
        {
            this.userService = userService;
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }

        public async Task<string> GetTokenAsunc(string email, string password)
        {
            if (email == null || password == null)
            {
                throw new ArgumentNullException("Empty username or password.");
            }

            using (MD5 md5Hash = MD5.Create())
            {
                password = Hash.GetMd5Hash(md5Hash, password);
            }
            User user = await userService.GetUserByEmailAsunc(email);

            var identity = GetIdentity(user);
            if (identity == null || user.Password != password)
            {
                throw new ArgumentException("Invalid username or password.");
            }

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
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
