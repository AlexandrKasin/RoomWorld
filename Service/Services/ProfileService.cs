using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Service.dto;

namespace Service.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Flat> _flatRepository;
        private readonly IRepository<User> _userRepository;

        public ProfileService(IMapper mapper, IRepository<User> userRepository, IRepository<Flat> flatRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _flatRepository = flatRepository;
        }

        public async Task<UserViewModel> GetProflieByEmailAsync(string email)
        {
            var user = await (await _userRepository.GetAllAsync(t => t.Email == email))
                .FirstOrDefaultAsync();
            user.Flats =
                    await (await _flatRepository.GetAllAsync(x => x.User.Id == user.Id, x => x.Location, x => x.Images,
                        x => x.Orders)).ToListAsync();
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task ChangeProfileAsync(UserViewModel user)
        {
            var currentUser =
                await (await _userRepository.GetAllAsync(x => x.Email == user.Email)).FirstOrDefaultAsync();
            if (currentUser != null)
            {
                currentUser.Name = user.Name;
                currentUser.Surname = user.Surname;
                currentUser.PhoneNumber = user.PhoneNumber;
                await _userRepository.UpdateAsync(currentUser);
            }
            
        }
    }
}