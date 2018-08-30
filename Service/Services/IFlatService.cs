using System;
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
        Task<IQueryable<Flat>> GetAllAsync(Expression<Func<Flat, bool>> predicate);
        Task UpdateFlatAsunc(Flat flat);
        Task DeleteFlatAsunc(Flat flat);
    }
}
