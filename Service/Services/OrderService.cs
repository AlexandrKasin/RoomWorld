using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Service.dto;

namespace Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IFlatService _flatService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public OrderService(IRepository<Order> orderRepository, IFlatService flatService, IUserService userService,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _flatService = flatService;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task AddOrderAsunc(OrderParams orderParams, string email)
        {
            var order = new Order
            {
                DateFrom = orderParams.DateFrom,
                DateTo = orderParams.DateTo,
                Flat = await (await _flatService.GetAllAsync(x => x.Id == orderParams.IdFlat)).FirstOrDefaultAsync(),
                Price = orderParams.Price,
                User = (await _userService.GetAllAsync(x => x.Email == email)).FirstOrDefault()
            };
            await _orderRepository.InsertAsync(order);
        }

        public async Task<IQueryable<Order>> GetAllAsync(Expression<Func<Order, bool>> predicate = null,
            params Expression<Func<Order, object>>[] includeParams)
        {
            return await _orderRepository.GetAllAsync(predicate, includeParams);
        }

        public async Task<IList<OrderedFlatViewModel>> GetOrdersByEmailAsync(string email)
        {
            var orders = (await _orderRepository.GetAllAsync(x => x.User.Email == email, x => x.Flat, x => x.User))
                .Include(x => x.Flat.Location)
                .Include(x => x.Flat.Images);
            return _mapper.Map<IList<OrderedFlatViewModel>>(orders);
        }

        public async Task<IList<OrderedFlatViewModel>> GetOrdersForUsersFlatsAsync(string email)
        {
            var orders = (await _orderRepository.GetAllAsync(x => x.Flat.User.Email == email, x => x.Flat, x => x.User))
                .Include(x => x.Flat.Location)
                .Include(x => x.Flat.Images);
            return _mapper.Map<IList<OrderedFlatViewModel>>(orders);
        }
    }
}