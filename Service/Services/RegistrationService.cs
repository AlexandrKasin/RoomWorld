using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RoomWorld;
using RoomWorld.Models;
using RoomWorld.Repositories;
using RoomWorld.Services;
using Service.iServices;

namespace Service.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRepository<User> repository;
        private readonly IUserService userService;


        public RegistrationService(IRepository<User> repository, IUserService userService)
        {
            this.repository = repository;
            this.userService = userService;
        }

        public async Task RegistrateUserAsunc(User user)
        {
            if (user.Name == null || user.Surname == null || user.Email == null || user.Password == null || user.PhoneNumber == null)
            {
                throw new ArgumentNullException("Some field are empty.");
            }

            User existingUser = await userService.GetUserByEmailAsunc(user.Email);
            if (existingUser != null)
            {
                throw new ArgumentException("This email already exists.");  
            }
            
            user.Role = "User";
            using (MD5 md5Hash = MD5.Create())
            {
                user.Password = HashService.GetMd5Hash(md5Hash, user.Password);
            }
            await repository.InsertAsunc(user);
        }
    }
}
