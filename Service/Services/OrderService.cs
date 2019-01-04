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
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Flat> _flatRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(IRepository<Order> orderRepository, IMapper mapper, IRepository<Flat> flatRepository,
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
                throw new EntityNotExistException("Flat not found.");
            }

            var order = new Order
            {
                DateFrom = orderParamsViewModel.DateFrom,
                DateTo = orderParamsViewModel.DateTo,
                Flat = flat,
                Price = orderParamsViewModel.Price,
                User = user,
                CreatedBy = user.Id
            };
            await _orderRepository.InsertAsync(order);
        }

        public async Task<IList<Order>> GetAllAsync(Expression<Func<Order, bool>> predicate = null,
            params Expression<Func<Order, object>>[] includeParams)
        {
            return await (await _orderRepository.GetAllAsync(predicate, includeParams)).ToListAsync();
        }

        public async Task<IList<OrderedFlatViewModel>> GetOrdersByEmailAsync()
        {
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;

            var orders = (await _orderRepository.GetAllAsync(x => x.User.Email == email, x => x.Flat, x => x.User,
                x => x.Flat.Location, x => x.Flat.Images));
            return _mapper.Map<IList<OrderedFlatViewModel>>(orders);
        }

        public async Task<IList<OrderedFlatViewModel>> GetOrdersForUsersFlatsAsync()
        {
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;

            var orders = (await _orderRepository.GetAllAsync(x => x.Flat.User.Email == email, x => x.Flat, x => x.User,
                x => x.Flat.Location, x => x.Flat.Images));
            return _mapper.Map<IList<OrderedFlatViewModel>>(orders);
        }
    }
}