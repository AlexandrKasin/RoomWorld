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
        Task AddOrderAsync(OrderParamsViewModel orderParamsViewModel);

        Task<IList<ApartmentReservation>> GetAllAsync(Expression<Func<ApartmentReservation, bool>> predicate = null,
            params Expression<Func<ApartmentReservation, object>>[] includeParams);

        Task<IList<OrderedFlatViewModel>> GetOrdersByEmailAsync();
        Task<IList<OrderedFlatViewModel>> GetOrdersForUsersFlatsAsync();
    }
}