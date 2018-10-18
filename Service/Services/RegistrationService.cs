using System.Security.Claims;
using System.Threading.Tasks;
using Data;
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

        public RegistrationService(IRepository<User> repositoryUser, IHashMd5Service hashMd5Service,
            ITokenService tokenService, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _repositoryUser = repositoryUser;
            _hashMd5Service = hashMd5Service;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<Token> RegisterUserAsync(User user)
        {
            var exists = await (await _repositoryUser.GetAllAsync(t => t.Email == user.Email)).AnyAsync();
            if (exists)
            {
                throw new EmailAlredyExistsException("This email already exists.");
            }

            var password = user.Password;
            user.Role = Role.User;
            var systemUser =
                await (await _repositoryUser.GetAllAsync(t => t.Email == _configuration["EmailSystemUser"]))
                    .FirstOrDefaultAsync();
            user.CreatedBy = systemUser.Id;
            user.Password = _hashMd5Service.GetMd5Hash(user.Password);
            await _repositoryUser.InsertAsync(user);

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