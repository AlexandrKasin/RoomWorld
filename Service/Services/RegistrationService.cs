using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using RoomWorld;
using Service.iServices;

namespace Service.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRepository<User> repository;

        public RegistrationService(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public async Task RegistrateUserAsunc(User user)
        {
            if (user.Name == null || user.Surname == null || user.Email == null || user.Password == null || user.PhoneNumber == null)
            {
                throw new ArgumentNullException("Some field are empty.");
            }
            
            User existingUser = await repository.GetAll().FirstOrDefaultAsync(t => t.Email == user.Email);
            if (existingUser != null)
            {
                throw new ArgumentException("This email already exists.");  
            }
            
            user.Role = "User";
            using (MD5 md5Hash = MD5.Create())
            {
                user.Password = Hash.GetMd5Hash(md5Hash, user.Password);
            }
            await repository.InsertAsunc(user);
        }
    }
}
