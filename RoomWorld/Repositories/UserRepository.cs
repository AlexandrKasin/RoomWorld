using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoomWorld.Models;

namespace RoomWorld.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext databaseContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public User GetUserByEmail(string email)
        {
            return databaseContext.Users.FirstOrDefault(s => s.Email == email);
        }
    }
}
