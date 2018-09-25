using System;
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
        private readonly IOrderService _orderService;

        public ProfileService(IUserService userService, IFlatService flatService, IOrderService orderService)
        {
            _userService = userService;
            _flatService = flatService;
            _orderService = orderService;
        }

        public async Task<User> GetProflieByEmail(string email)
        {
            var user = await (await _userService.GetAllAsync(t => t.Email == email))
                .FirstOrDefaultAsync();
            user.Flats =
                new List<Flat>(
                    await _flatService.GetAllAsync(x => x.User.Id == user.Id, x => x.Location, x => x.Images));
            user.Orders = new List<Order>(await _orderService.GetAllAsync(x => x.User.Id == user.Id, x => x.Flat, x => x.User));
            return user;
        }
    }
}