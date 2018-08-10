using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RoomWorld.Models;
using RoomWorld.Repositories;

namespace RoomWorld.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> repository;


        public UserService(IRepository<User> repository)
        {
            this.repository = repository;
        }
        public void AddUser(User user)
        {
            if (user.Name == null || user.Surname == null || user.Email == null || user.Password == null || user.PhoneNumber == null)
            {
                throw new ArgumentNullException("Some field are empty.");
            }

            User existingUser = GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                throw new ArgumentException("This email already exists.");  
            }
            
            user.Role = "User";
            using (MD5 md5Hash = MD5.Create())
            {
                user.Password = Hash.GetMd5Hash(md5Hash, user.Password);
            }
            repository.Insert(user);
        }

        public User GetUserById(int id)
        {
            return repository.GetById(id);
        }

        public IEnumerable<User> GetAll()
        {
            return repository.GetAll();
        }

        public User GetUserByEmail(string email)
        {
            return repository.GetAll().FirstOrDefault(t=>t.Email == email);
        }

        public ClaimsIdentity GetIdentity(User user)
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

        public string GetToken(string email, string password)
        {
            if (email == null || password == null)
            {
               throw new ArgumentNullException("Empty username or password.");   
            }

            using (MD5 md5Hash = MD5.Create())
            {
                password = Hash.GetMd5Hash(md5Hash, password);
            }
            User user = GetUserByEmail(email);

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
