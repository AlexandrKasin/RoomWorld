using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RoomWorld.Models;

namespace RoomWorld.Repositories
{
    public class Repository<T> : IRepository<T> where T : class 
    {
        private readonly DatabaseContext databaseContext;
        private DbSet<T> entities;

        public Repository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
            entities = databaseContext.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return entities.AsQueryable();
        }

        public T GetById(int id)
        {
            return entities.Find(id);
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            databaseContext.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            databaseContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            databaseContext.SaveChanges();
        }

        public void SaveChanges()
        {
            databaseContext.SaveChanges();
        }
    }
}
