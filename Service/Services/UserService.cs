using System;
using System.Collections;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;


        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task AddUserAsync(User user)
        {
            await _repository.InsertAsync(user);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IList> GetAllAsync(Expression<Func<User, bool>> predicate = null, params Expression<Func<User, object>>[] includeParams)
        {
            return await (await _repository.GetAllAsync(predicate, includeParams)).ToListAsync();
        }


        public async Task UpdateUserAsync(User user)
        {
            await _repository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(User user)
        {
            await _repository.DeleteAsync(user);
        } 
    }
}
