using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Data.Entity;
using Repository.Repositories;
using RoomWorld;
using Service.iServices;

namespace Service.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRepository<User> _repositoryService;

        public RegistrationService(IRepository<User> repositoryService)
        {
            _repositoryService = repositoryService;
        }

        public async Task RegistrateUserAsunc(User user)
        {
            if (user.Name == null || user.Surname == null || user.Email == null || user.Password == null || user.PhoneNumber == null)
            {
                throw new ArgumentNullException("Some field are empty.");
            }
            
            ICollection<User> existingUser = await _repositoryService.GetAllAsync(t => t.Email == user.Email);
            if (existingUser.First() != null)
            {
                throw new ArgumentException("This email already exists.");  
            }
            
            user.Role = "User";
            using (MD5 md5Hash = MD5.Create())
            {
                user.Password = Hash.GetMd5Hash(md5Hash, user.Password);
            }
            await _repositoryService.InsertAsync(user);
        }
    }
}
