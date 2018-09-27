using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Service.dto;

namespace Service.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserService _userService;
        private readonly IFlatService _flatService;
        private readonly IMapper _mapper;

        public ProfileService(IUserService userService, IFlatService flatService,
            IMapper mapper)
        {
            _userService = userService;
            _flatService = flatService;
            _mapper = mapper;
        }

        public async Task<UserViewModel> GetProflieByEmail(string email)
        {
            var user = await (await _userService.GetAllAsync(t => t.Email == email))
                .FirstOrDefaultAsync();
            user.Flats =
                new List<Flat>(
                    await _flatService.GetAllAsync(x => x.User.Id == user.Id, x => x.Location, x => x.Images));
            return _mapper.Map<UserViewModel>(user);
        }
    }
}