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
        private readonly ITokenService _tokenService;

        public RegistrationService(IRepository<User> repositoryService, IHashMd5Service hashMd5Service, ITokenService tokenService)
        {
            _repositoryService = repositoryService;
            _hashMd5Service = hashMd5Service;
            _tokenService = tokenService;
        }

        public async Task<Token> RegistrateUserAsunc(User user)
        {
            var password = user.Password;
            user.Role = Role.User.ToString();
            var systemUser = await _repositoryService.GetAllAsync(t => t.Email == "system@admin.com").Result
                .FirstOrDefaultAsync();
            user.CreatedBy = systemUser.Id;

            var exists = (await _repositoryService.GetAllAsync(t => t.Email == user.Email)).Any();
            if (exists)
            {
                throw new ArgumentException("This email already exists.");
            }

            user.Password = _hashMd5Service.GetMd5Hash(user.Password);
            await _repositoryService.InsertAsync(user);
            var token = await _tokenService.GetTokenAsunc(new Authorize() { Email = user.Email, Password = password });
            return token;
        }

        public async Task ChangePasswordAsync(ChangePasswordParams changePasswordParams, string email)
        {
            var user = await (await _repositoryService.GetAllAsync(x =>
                    x.Email == email && x.Password == _hashMd5Service.GetMd5Hash(changePasswordParams.CurrentPassword)))
                .FirstOrDefaultAsync();
            user.Password = _hashMd5Service.GetMd5Hash(changePasswordParams.NewPassword);
            if(user == null)
            {
                throw new ArgumentException("Incorrect password");
            }
            await _repositoryService.UpdateAsync(user);
        }
    }
}