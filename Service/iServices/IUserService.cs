using System.Linq;
using System.Threading.Tasks;
using Data.Entity;

namespace Service.iServices
{
    public interface IUserService
    {
        Task AddUserAsunc(User user);
        Task<User> GetUserByIdAsunc(int id);
        IQueryable<User> GetAll();
        Task UpdateUserAsunc(User user);
        Task DeleteUserAsunc(User user);
    
    }
}
