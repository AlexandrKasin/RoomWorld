using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Entity;
using Service.dto;

namespace Service.Services
{
    public interface IOrderService
    {
        Task AddOrderAsync(OrderParams orderParams, string email);

        Task<IList<Order>> GetAllAsync(Expression<Func<Order, bool>> predicate = null,
            params Expression<Func<Order, object>>[] includeParams);

        Task<IList<OrderedFlatViewModel>> GetOrdersByEmailAsync(string email);
        Task<IList<OrderedFlatViewModel>> GetOrdersForUsersFlatsAsync(string email);
    }
}