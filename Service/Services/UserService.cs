using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RoomWorld.Models;
using RoomWorld.Repositories;

namespace RoomWorld.Services
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

        public async Task<User> GetUserByEmailAsunc(string email)
        {
            return await repository.GetAll().FirstOrDefaultAsync(t=>t.Email == email);
        }

       
    }
}
