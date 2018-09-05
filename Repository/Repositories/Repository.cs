using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DatabaseContext _databaseContext;
        private readonly DbSet<T> _entitySet;

        public Repository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _entitySet = databaseContext.Set<T>();
        }

        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
            params Expression<Func<T, object>>[] includeParams)
        {
            if (predicate is null)
            {
                return includeParams.Aggregate(_entitySet.AsQueryable(),
                    (current, include) => current.Include(include));
            }

            return await Task.Run(() =>
                includeParams.Aggregate(_entitySet.Where(predicate), (current, include) => current.Include(include)));         
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entitySet.FindAsync(id);
        }

        public async Task InsertAsync(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            await _entitySet.AddAsync(entity);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            await _databaseContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            _entitySet.Remove(entity);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }
    }
}