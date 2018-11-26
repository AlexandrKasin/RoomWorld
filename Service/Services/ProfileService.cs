using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Repositories;
using Service.dto;
using Service.DTO;
using Service.Exceptions;
using Service.Extension;

namespace Service.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Flat> _flatRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IHashMd5Service _hashMd5Service;

        public ProfileService(IMapper mapper, IRepository<User> userRepository, IRepository<Flat> flatRepository,
            IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IEmailService emailService,
            IHashMd5Service hashMd5Service)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _flatRepository = flatRepository;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _emailService = emailService;
            _hashMd5Service = hashMd5Service;
        }

        public async Task<ProfileViewModel> GetProflieByEmailAsync()
        {
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            var user = await (await _userRepository.GetAllAsync(t => t.Email == email))
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new IncorrectAuthParamsException("Email does not exist.");
            }
            return _mapper.Map<ProfileViewModel>(user);
        }

        public async Task ChangeProfileAsync(ProfileViewModel user)
        {
            var currentUser =
                await (await _userRepository.GetAllAsync(x => x.Email == user.Email)).FirstOrDefaultAsync();
            if (currentUser == null)
            {
                throw new IncorrectAuthParamsException("User does not exist.");
            }

            currentUser.Name = user.Name;
            currentUser.Surname = user.Surname;
            currentUser.PhoneNumber = user.PhoneNumber;
            await _userRepository.UpdateAsync(currentUser);
        }

        public async Task SendMessageResetPasswordAsync(string email)
        {
            var isExsist = await (await _userRepository.GetAllAsync(user => user.Email == email)).AnyAsync();
            if (!isExsist)
                throw new EntityNotExistException("This email is not exist");
            var tokenToEncrypt = email + "|" + DateTime.Now.AddMinutes(10);
            var encryptedText = Encrypting.Encrypt(tokenToEncrypt, _configuration["EncryptionKey"], true);
            encryptedText = HttpUtility.UrlEncode(encryptedText);
            
            var resetPasswordView = File.ReadAllText(@"..\Service\Templates\View\ResetPassword.html");
            await _emailService.SendEmailAsync(email, "Password reset", resetPasswordView);
        }

        public async Task ResetPasswordByTokenAsync(ResetPasswordViewModel model)
        {
            var decryptedToken = await Task.Run(() =>
                Encrypting.Decrypt(HttpUtility.UrlDecode(model.Token), _configuration["EncryptionKey"], true));
            var tokenParts = decryptedToken.Split("|");
            if (tokenParts.Length < 2)
                throw new InvalidTokenException("Token is not valid");
            var email = tokenParts[0];
            var expireTime = tokenParts[1];

            if (DateTime.Compare(DateTime.Now, DateTime.Parse(expireTime)) == 1)
                throw new TokenExpiredException("Token is expired");

            var user = await (await _userRepository.GetAllAsync(u => u.Email == email)).FirstOrDefaultAsync();
            if (user == null)
                throw new EntityNotExistException("This user is not exist");

            user.Password = _hashMd5Service.GetMd5Hash(model.Password);
            await _userRepository.UpdateAsync(user);
        }
    }
}