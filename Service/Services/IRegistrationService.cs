using System.Threading.Tasks;
using Data.Entity;

namespace Service.Services
{
    public interface IRegistrationService
    {
        Task RegistrateUserAsunc(User user);
    }
}
