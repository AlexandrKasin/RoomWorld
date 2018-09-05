using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Entity;

namespace Service.Services
{
    public interface IUserService
    {
        Task AddUserAsunc(User user);
        Task<User> GetUserByIdAsunc(int id);
        Task<IQueryable<User>> GetAllAsync(Expression<Func<User, bool>> predicate, params Expression<Func<User, object>>[] includeParams);
        Task UpdateUserAsunc(User user);
        Task DeleteUserAsunc(User user);
    
    }
}
