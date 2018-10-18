using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Service.dto;
using Service.Exceptions;

namespace Service.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Flat> _flatRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileService(IMapper mapper, IRepository<User> userRepository, IRepository<Flat> flatRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _flatRepository = flatRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserViewModel> GetProflieByEmailAsync()
        {
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            var user = await (await _userRepository.GetAllAsync(t => t.Email == email))
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new IncorrectAuthParamsException("Email does not exist.");
            }

            user.Flats =
                await (await _flatRepository.GetAllAsync(x => x.User.Id == user.Id, x => x.Location, x => x.Images,
                    x => x.Orders)).ToListAsync();
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task ChangeProfileAsync(UserViewModel user)
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
    }
}