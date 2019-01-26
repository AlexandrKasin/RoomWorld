using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class OrderService : IOrderService
    {
        private readonly IRepository<ApartmentReservation> _orderRepository;
        private readonly IRepository<Apartment> _flatRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(IRepository<ApartmentReservation> orderRepository, IMapper mapper, IRepository<Apartment> flatRepository,
            IRepository<User> userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _flatRepository = flatRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddOrderAsync(OrderParamsViewModel orderParamsViewModel)
        {
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;

            var user = (await _userRepository.GetAllAsync(x => x.Email == email)).FirstOrDefault();
            if (user == null)
            {
                throw new IncorrectParamsException("User does not exist.");
            }

            var flat = await (await _flatRepository.GetAllAsync(x => x.Id == orderParamsViewModel.IdFlat))
                .FirstOrDefaultAsync();

            if (flat == null)
            {
                throw new EntityNotExistException("Apartment not found.");
            }

            var order = new ApartmentReservation
            {
                DateFrom = orderParamsViewModel.DateFrom,
                DateTo = orderParamsViewModel.DateTo,
                Apartment = flat,
                SumPrice = orderParamsViewModel.Price,
                Client = user,
                CreatedBy = user.Id
            };
            await _orderRepository.InsertAsync(order);
        }

        public async Task<IList<ApartmentReservation>> GetAllAsync(Expression<Func<ApartmentReservation, bool>> predicate = null,
            params Expression<Func<ApartmentReservation, object>>[] includeParams)
        {
            return await (await _orderRepository.GetAllAsync(predicate, includeParams)).ToListAsync();
        }

        public async Task<IList<OrderedFlatViewModel>> GetOrdersByEmailAsync()
        {
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;

            var orders = (await _orderRepository.GetAllAsync(x => x.Client.Email == email, x => x.Apartment, x => x.Client,
                x => x.Apartment.ApartmentLocation, x => x.Apartment.ApartmentImages));
            return _mapper.Map<IList<OrderedFlatViewModel>>(orders);
        }

        public async Task<IList<OrderedFlatViewModel>> GetOrdersForUsersFlatsAsync()
        {
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;

            var orders = (await _orderRepository.GetAllAsync(x => x.Apartment.Owner.Email == email, x => x.Apartment, x => x.Client,
                x => x.Apartment.ApartmentLocation, x => x.Apartment.ApartmentImages));
            return _mapper.Map<IList<OrderedFlatViewModel>>(orders);
        }
    }
}