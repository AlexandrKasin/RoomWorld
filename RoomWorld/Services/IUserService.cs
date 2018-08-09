using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using RoomWorld.Models;

namespace RoomWorld.Services
{
    public interface IUserService
    {
        void AddUser(User user);
        User GetUserById(int id);
        IEnumerable<User> GetAll();
        User GetUserByEmail(string email);
        ClaimsIdentity GetIdentity(User user);
    }
}
