using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Entity;

namespace Service.iServices
{
    public interface IUserService
    {
        Task AddUserAsunc(User user);
        Task<User> GetUserByIdAsunc(int id);
        Task<ICollection<User>> GetAllAsync(Expression<Func<User, bool>> predicate);
        Task UpdateUserAsunc(User user);
        Task DeleteUserAsunc(User user);
    
    }
}
