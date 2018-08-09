using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoomWorld.Models;

namespace RoomWorld.Repositories
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);
    }
}
