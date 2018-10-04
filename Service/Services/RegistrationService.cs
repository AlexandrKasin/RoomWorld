using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Service.dto;

namespace Service.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRepository<User> _repositoryUser;
        private readonly IHashMd5Service _hashMd5Service;
        private readonly ITokenService _tokenService;

        public RegistrationService(IRepository<User> repositoryUser, IHashMd5Service hashMd5Service, ITokenService tokenService)
        {
            _repositoryUser = repositoryUser;
            _hashMd5Service = hashMd5Service;
            _tokenService = tokenService;
        }

        public async Task<Token> RegistrateUserAsunc(User user)
        {
            var password = user.Password;
            user.Role = (int)Role.User;
            var systemUser = await _repositoryUser.GetAllAsync(t => t.Email == "system@admin.com").Result
                .FirstOrDefaultAsync();
            user.CreatedBy = systemUser.Id;

            var exists = (await _repositoryUser.GetAllAsync(t => t.Email == user.Email)).Any();
            if (exists)
            {
                throw new ArgumentException("This email already exists.");
            }

            user.Password = _hashMd5Service.GetMd5Hash(user.Password);
            await _repositoryUser.InsertAsync(user);
            var token = await _tokenService.GetTokenAsunc(new Authorize() { Email = user.Email, Password = password });
            return token;
        }

        public async Task ChangePasswordAsync(ChangePasswordParams changePasswordParams, string email)
        {
            var user = await (await _repositoryUser.GetAllAsync(x =>
                    x.Email == email && x.Password == _hashMd5Service.GetMd5Hash(changePasswordParams.CurrentPassword)))
                .FirstOrDefaultAsync();
            user.Password = _hashMd5Service.GetMd5Hash(changePasswordParams.NewPassword);
            if(user == null)
            {
                throw new ArgumentException("Incorrect password");
            }
            await _repositoryUser.UpdateAsync(user);
        }

       
    }
}