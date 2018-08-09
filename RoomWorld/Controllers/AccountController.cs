using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RoomWorld.Models;
using RoomWorld.Repositories;
using RoomWorld.Services;

namespace RoomWorld.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("/registration")]
        public async Task Registration(User user)
        {
            if (user.Name == null || user.Surname == null || user.Email == null || user.Password == null || user.PhoneNumber ==null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Some field are empty.");
                return;
            }

            User existingUser = userService.GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("This email already exists.");
                return;
            }

            userService.AddUser(user);
            Response.StatusCode = 200;
            await Response.WriteAsync("Succesfull");
        }

        [HttpPost("/token")]
        public async Task Token(string email, string password, [FromServices]DatabaseContext databaseContext)
        {
            if (email == null || password == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Empty username or password.");
                return;
            }

            using (MD5 md5Hash = MD5.Create())
            {
                password = Hash.GetMd5Hash(md5Hash, password);
            }
            User user = databaseContext.Users.FirstOrDefault(d => d.Email == email && d.Password == password);

            var identity = GetIdentity(user);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
                return;
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

            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
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
    }
}