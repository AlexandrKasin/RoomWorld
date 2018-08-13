using System.Threading.Tasks;
using Data.Entity;

namespace Service.iServices
{
    public interface IRegistrationService
    {
        Task RegistrateUserAsunc(User user);
    }
}
