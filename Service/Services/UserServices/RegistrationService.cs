using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Data;
using Data.Entity;
using Data.Entity.UserEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Repositories;
using Service.DTO;
using Service.DTO.UserDTO;
using Service.Exceptions;

namespace Service.Services.UserServices
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRepository<User> _repositoryUser;
        private readonly IHashMd5 _hashMd5Service;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IRepository<Role> _repositoryRole;
        private readonly IRepository<UserRoles> _repositoryUserRoles;
        private readonly IMapper _mapper;

        public RegistrationService(IRepository<User> repositoryUser, IHashMd5 hashMd5Service,
            ITokenService tokenService, IHttpContextAccessor httpContextAccessor, IConfiguration configuration,
            IRepository<Role> repositoryRole, IRepository<UserRoles> repositoryUserRoles, IMapper mapper)
        {
            _repositoryUser = repositoryUser;
            _hashMd5Service = hashMd5Service;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _repositoryRole = repositoryRole;
            _repositoryUserRoles = repositoryUserRoles;
            _mapper = mapper;
        }

        public async Task<TokenDTO> RegisterUserAsync(UserRegistrationDTO userParams)
        {
            var user = _mapper.Map<User>(userParams);
            var existsEmail =
                await (await _repositoryUser.GetAllAsync(t => t.Email.ToUpper().Equals(user.Email.ToUpper())))
                    .AnyAsync();
            if (existsEmail)
            {
                throw new EntityAlredyExistsException("This email already exists.");
            }

            var existsUsername =
                await (await _repositoryUser.GetAllAsync(t => t.Username.ToUpper().Equals(user.Username.ToUpper())))
                    .AnyAsync();
            if (existsUsername)
            {
                throw new EntityAlredyExistsException("This username already exists.");
            }

            var role = await (await _repositoryRole.GetAllAsync(r =>
                r.Name.ToUpper().Equals(RolesEnum.User.ToString().ToUpper()))).FirstOrDefaultAsync();
            if (role == null)
            {
                throw new EntityNotExistException("Role - user, not exists.");
            }

            var systemUser =
                await (await _repositoryUser.GetAllAsync(t =>
                        t.Email.ToUpper().Equals(_configuration["EmailSystemUser"].ToUpper())))
                    .FirstOrDefaultAsync();
            if (systemUser == null)
            {
                throw new EntityNotExistException("System user with email: " + _configuration["EmailSystemUser"] +
                                                  "not exist");
            }

            user.CreatedBy = systemUser.Id;
            user.Password = _hashMd5Service.GetMd5Hash(user.Password);
            await _repositoryUserRoles.InsertAsync(new UserRoles {User = user, Role = role, CreatedBy = systemUser.Id});

            var token = await _tokenService.GetTokenAsync(new AuthorizeDTO
            {
                Email = user.Email,
                Password = userParams.Password
            });
            return token;
        }

        public async Task ChangePasswordAsync(ChangePasswordParamsDTO changePasswordParams)
        {
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            var user = await (await _repositoryUser.GetAllAsync(x =>
                    x.Email == email && x.Password == _hashMd5Service.GetMd5Hash(changePasswordParams.CurrentPassword)))
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new IncorrectParamsException("Incorrect current password.");
            }

            user.Password = _hashMd5Service.GetMd5Hash(changePasswordParams.NewPassword);
            await _repositoryUser.UpdateAsync(user);
        }
    }
}