using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using RoomWorld.Models;

namespace RoomWorld.Services
{
    public interface IUserService
    {
        Task AddUserAsunc(User user);
        Task<User> GetUserByIdAsunc(int id);
        IQueryable<User> GetAll();
        Task UpdateUserAsunc(User user);
        Task DeleteUserAsunc(User user);
        Task<User> GetUserByEmailAsunc(string email);
    
    }
}
