using System.Collections.Generic;
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
        private readonly IUserService _userService;
        private readonly IFlatService _flatService;
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepository;

        public ProfileService(IUserService userService, IFlatService flatService,
            IMapper mapper, IRepository<User> userRepository)
        {
            _userService = userService;
            _flatService = flatService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> GetProflieByEmailAsync(string email)
        {
            var user = await (await _userService.GetAllAsync(t => t.Email == email))
                .FirstOrDefaultAsync();
            user.Flats =
                new List<Flat>(
                    await _flatService.GetAllAsync(x => x.User.Id == user.Id, x => x.Location, x => x.Images,
                        x => x.Orders));
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task ChangeProfileAsync(UserViewModel user)
        {
            var currentUser =
                await (await _userRepository.GetAllAsync(x => x.Email == user.Email)).FirstOrDefaultAsync();
            currentUser.Name = user.Name;
            currentUser.Surname = user.Surname;
            currentUser.PhoneNumber = user.PhoneNumber;
            await _userRepository.UpdateAsync(currentUser);
        }
    }
}