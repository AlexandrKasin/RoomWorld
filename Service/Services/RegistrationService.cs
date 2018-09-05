using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace Service.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRepository<User> _repositoryService;
        private readonly IHashMd5Service _hashMd5Service;

        public RegistrationService(IRepository<User> repositoryService, IHashMd5Service hashMd5Service)
        {
            _repositoryService = repositoryService;
            _hashMd5Service = hashMd5Service;
        }

        public async Task RegistrateUserAsunc(User user)
        {
            user.Role = Role.User.ToString();
            var systemUser = await _repositoryService.GetAllAsync(t=>t.Email == "system@admin.com").Result.FirstAsync();
            user.CreatedBy = systemUser.Id;
            
            var exists = (await _repositoryService.GetAllAsync(t => t.Email == user.Email)).Any();
            if (exists)
            {
                throw new ArgumentException("This email already exists.");  
            }
            
            user.Password = _hashMd5Service.GetMd5Hash(user.Password);
            await _repositoryService.InsertAsync(user);
        }
    }
}
