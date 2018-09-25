using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Data.Entity;

namespace Service.Services
{
    public interface IOrderService
    {
        Task AddOrderAsunc(OrderParams orderParams, string email);
        Task<IQueryable<Order>> GetAllAsync(Expression<Func<Order, bool>> predicate = null, params Expression<Func<Order, object>>[] includeParams);

    }
}
