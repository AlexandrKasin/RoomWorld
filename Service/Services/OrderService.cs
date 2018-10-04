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
        private readonly IRepository<Flat> _flatRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public OrderService(IRepository<Order> orderRepository, IMapper mapper, IRepository<Flat> flatRepository,
            IRepository<User> userRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _flatRepository = flatRepository;
            _userRepository = userRepository;
        }

        public async Task AddOrderAsunc(OrderParams orderParams, string email)
        {
            var order = new Order
            {
                DateFrom = orderParams.DateFrom,
                DateTo = orderParams.DateTo,
                Flat = await (await _flatRepository.GetAllAsync(x => x.Id == orderParams.IdFlat)).FirstOrDefaultAsync(),
                Price = orderParams.Price,
                User = (await _userRepository.GetAllAsync(x => x.Email == email)).FirstOrDefault()
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