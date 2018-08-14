using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Service.iServices;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> repository;


        public UserService(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public async Task AddUserAsunc(User user)
        {
            await repository.InsertAsunc(user);
        }

        public async Task<User> GetUserByIdAsunc(int id)
        {
            return await repository.GetByIdAsunc(id);
        }

        public IQueryable<User> GetAll()
        {
            return repository.GetAll();
        }

        public async Task UpdateUserAsunc(User user)
        {
            await repository.UpdateAsunc(user);
        }

        public async Task DeleteUserAsunc(User user)
        {
            await repository.DeleteAsunc(user);
        } 
    }
}
