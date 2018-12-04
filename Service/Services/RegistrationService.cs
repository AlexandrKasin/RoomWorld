using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Repositories;
using Service.Exceptions;

namespace Service.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRepository<User> _repositoryUser;
        private readonly IHashMd5Service _hashMd5Service;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IRepository<Role> _repositoryRole;
        private readonly IRepository<UserRoles> _repositoryUserRoles;

        public RegistrationService(IRepository<User> repositoryUser, IHashMd5Service hashMd5Service,
            ITokenService tokenService, IHttpContextAccessor httpContextAccessor, IConfiguration configuration,
            IRepository<Role> repositoryRole, IRepository<UserRoles> repositoryUserRoles)
        {
            _repositoryUser = repositoryUser;
            _hashMd5Service = hashMd5Service;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _repositoryRole = repositoryRole;
            _repositoryUserRoles = repositoryUserRoles;
        }

        public async Task<Token> RegisterUserAsync(User user)
        {
            /*var a = await (await _repositoryUser.GetAllAsync(t => t.Email == user.Email, i => i.UserRoles)).Include("UserRoles.Role").FirstOrDefaultAsync();*/
            /*var b = await _repositoryUserRoles.GetAllAsync(null, t => t.User, t => t.Role);*/
            var exists = await (await _repositoryUser.GetAllAsync(t => t.Email == user.Email)).AnyAsync();
            if (exists)
            {
                throw new EmailAlredyExistsException("This email already exists.");
            }

            var role = await (await _repositoryRole.GetAllAsync(r =>
                string.Equals(r.Name, "User", StringComparison.CurrentCultureIgnoreCase))).FirstOrDefaultAsync();
            var password = user.Password;
            var systemUser =
                await (await _repositoryUser.GetAllAsync(t => t.Email == _configuration["EmailSystemUser"])).FirstOrDefaultAsync();
            user.CreatedBy = systemUser.Id;
            user.Password = _hashMd5Service.GetMd5Hash(user.Password);
            await _repositoryUserRoles.InsertAsync(new UserRoles {User = user, Role = role});
            //await _repositoryUser.InsertAsync(user);


            var token = await _tokenService.GetTokenAsync(new Authorize {Email = user.Email, Password = password});
            return token;
        }

        public async Task ChangePasswordAsync(ChangePasswordParams changePasswordParams)
        {
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;

            var user = await (await _repositoryUser.GetAllAsync(x =>
                    x.Email == email && x.Password == _hashMd5Service.GetMd5Hash(changePasswordParams.CurrentPassword)))
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new IncorrectAuthParamsException("Incorrect email or password");
            }

            user.Password = _hashMd5Service.GetMd5Hash(changePasswordParams.NewPassword);
            await _repositoryUser.UpdateAsync(user);
        }
    }
}