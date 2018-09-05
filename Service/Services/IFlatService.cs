using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Entity;

namespace Service.Services
{
    public interface IFlatService
    {
        Task AddFlatAsunc(Flat flat);
        Task<Flat> GetFlatByIdAsunc(int id);
        Task<IQueryable<Flat>> GetAllAsync(Expression<Func<Flat, bool>> predicate = null, params Expression<Func<Flat, object>>[] includeParams);
        Task UpdateFlatAsunc(Flat flat);
        Task DeleteFlatAsunc(Flat flat);
    }
}
