using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomWorld.Repositories
{
    interface IRepository<T> where T : class 
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();
    }
}
