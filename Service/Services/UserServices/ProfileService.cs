using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Data.Entity.UserEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Repositories;
using Service.DTO;
using Service.Exceptions;
using Service.Extension;

namespace Service.Services.UserServices
{
    public class ProfileService : IProfileService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IHashMd5 _hashMd5Service;

        public ProfileService(IMapper mapper, IRepository<User> userRepository,
            IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IEmailService emailService,
            IHashMd5 hashMd5Service)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _emailService = emailService;
            _hashMd5Service = hashMd5Service;
        }

        public async Task<ProfileDTO> GetProflieByEmailAsync()
        {
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            var user = await (await _userRepository.GetAllAsync(t => t.Email.ToUpper().Equals(email.ToUpper())))
                .FirstOrDefaultAsync();
            if (user == null)
                throw new IncorrectParamsException("Email does not exist.");
            return _mapper.Map<ProfileDTO>(user);
        }

        public async Task ChangeProfileAsync(ProfileDTO user)
        {
            var currentUser =
                await (await _userRepository.GetAllAsync(x => x.Email.ToUpper().Equals(user.Email.ToUpper())))
                    .FirstOrDefaultAsync();
            if (currentUser == null)
                throw new IncorrectParamsException("User does not exist.");
            currentUser.Name = user.Name;
            currentUser.Surname = user.Surname;
            currentUser.PhoneNumber = user.PhoneNumber;
            currentUser.Version = user.Version;
            await _userRepository.UpdateAsync(currentUser);
        }

        public async Task SendMessageResetPasswordAsync(string email)
        {
            var isExsist =
                await (await _userRepository.GetAllAsync()).AnyAsync(user =>
                    user.Email.ToUpper().Equals(email.ToUpper()));
            if (!isExsist)
                throw new EntityNotExistException("This email is not exist");
            var tokenToEncrypt = email + "|" +
                                 DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["TimeToResetPassword"]));
            var encryptedText = Encrypting.Encrypt(tokenToEncrypt, _configuration["EncryptionKey"], true);
            encryptedText = HttpUtility.UrlEncode(encryptedText);
            /*var resetPasswordView = File.ReadAllText(@"..\Service\Templates\View\ResetPassword.html");*/
            await _emailService.SendEmailAsync(email, "Password reset",
                $"<a href={_configuration["AuthOption:Issuer"]}/change/password/{encryptedText}>Reset password</a>");
        }

        public async Task ResetPasswordByTokenAsync(ResetPasswordDTO model)
        {
            var decryptedToken =
                Encrypting.Decrypt(HttpUtility.UrlDecode(model.Token), _configuration["EncryptionKey"], true);
            var tokenParts = decryptedToken.Split("|");
            if (tokenParts.Length < 2)
                throw new InvalidDataException("TokenViewModel is not valid");
            var email = tokenParts[0];
            var expireTime = tokenParts[1];

            if (DateTime.Compare(DateTime.UtcNow, DateTime.Parse(expireTime)) == 1)
                throw new TokenExpiredException("TokenViewModel is expired");

            var user = await (await _userRepository.GetAllAsync(u => u.Email == email)).FirstOrDefaultAsync();
            if (user == null)
                throw new EntityNotExistException("This user is not exist");

            user.Password = _hashMd5Service.GetMd5Hash(model.Password);
            await _userRepository.UpdateAsync(user);
        }
    }
}