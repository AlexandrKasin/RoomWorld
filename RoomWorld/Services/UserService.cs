using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using RoomWorld.Models;
using RoomWorld.Repositories;

namespace RoomWorld.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> repository;

        private readonly IUserRepository userRepository;

        public UserService(IRepository<User> repository, IUserRepository userRepository)
        {
            this.repository = repository;
            this.userRepository = userRepository;
        }
        public void AddUser(User user)
        {
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
            return userRepository.GetUserByEmail(email);
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
    }
}
