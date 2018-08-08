using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RoomWorld.Models;

namespace RoomWorld.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private RoomWorldDatabaseContext databaseContext;

        public UserRepository()
        {
            this.databaseContext = new RoomWorldDatabaseContext();
        }
        public IEnumerable<User> GetAll()
        {
            return databaseContext.Users;
        }

        public User GetById(int id)
        {
            return databaseContext.Users.Find(id);
        }

        public void Insert(User user)
        {
            databaseContext.Users.Add(user);
        }

        public void Update(User user)
        {
            databaseContext.Entry(user).State = EntityState.Modified;
        }

        public void Delete(User user)
        {
            User existingUser = databaseContext.Users.Find(user.Id);
            if (existingUser != null)
            {
                databaseContext.Users.Remove(user);
            }
        }

        public void SaveChanges()
        {
            databaseContext.SaveChanges();
        }
    }
}
