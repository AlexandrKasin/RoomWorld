using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomWorld.Repositories
{
   public interface IRepository<T> where T : class 
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsunc(int id);
        Task InsertAsunc(T entity);
        Task UpdateAsunc(T entity);
        Task DeleteAsunc(T entity);
        Task SaveChangesAsunc();
    }
}
