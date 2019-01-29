using System;
using System.Collections;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Entity.UserEntity;

namespace Service.Services.UserServices
{
    public interface IUserService
    {
        Task AddUserAsync(User user);
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<IList> GetAllAsync(Expression<Func<User, bool>> predicate, params Expression<Func<User, object>>[] includeParams);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
    
    }
}
