using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Entity;
using Repository.Repositories;
using Service.iServices;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;


        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task AddUserAsunc(User user)
        {
            await _repository.InsertAsync(user);
        }

        public async Task<User> GetUserByIdAsunc(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public Task<ICollection<User>> GetAllAsync(Expression<Func<User, bool>> predicate)
        {
            return _repository.GetAllAsync(predicate);
        }


        public async Task UpdateUserAsunc(User user)
        {
            await _repository.UpdateAsync(user);
        }

        public async Task DeleteUserAsunc(User user)
        {
            await _repository.DeleteAsync(user);
        } 
    }
}
