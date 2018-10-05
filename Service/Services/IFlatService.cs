using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Entity;
using Service.dto;

namespace Service.Services
{
    public interface IFlatService
    {
        Task AddFlatAsync(Flat flat, string email);
        Task<FlatViewModel> GetFlatByIdAsync(int id);

        Task<ICollection<Flat>> GetAllAsync(Expression<Func<Flat, bool>> predicate = null,
            params Expression<Func<Flat, object>>[] includeParams);

        Task UpdateFlatAsync(Flat flat);
        Task DeleteFlatAsync(Flat flat);
        Task<ICollection<FlatViewModel>> SearchFlatAsync(SearchParams searchParams);
        Task<int> AmountFlatByParamsAsync(SearchParams searchParams);
    }
}