using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _repository;
        private readonly IFlatService _flatService;
        private readonly IUserService _userService;

        public OrderService(IRepository<Order> repository, IFlatService flatService, IUserService userService)
        {
            _repository = repository;
            _flatService = flatService;
            _userService = userService;
        }
        public async Task AddOrderAsunc(OrderParams orderParams, string email)
        {
            var order = new Order
            {
                DateFrom = orderParams.DateFrom,
                DateTo = orderParams.DateTo,
                Flat = await (await _flatService.GetAllAsync(x=>x.Id == orderParams.IdFlat)).FirstOrDefaultAsync(),
                Price = orderParams.Price,
                User = (await _userService.GetAllAsync(x=> x.Email == email)).FirstOrDefault()
            };
            await _repository.InsertAsync(order);
        }
    }
}
