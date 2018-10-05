using System.Threading.Tasks;
using Data.Entity;

namespace Service.Services
{
    public interface IRegistrationService
    {
        Task<Token> RegistrateUserAsync(User user);
        Task ChangePasswordAsync(ChangePasswordParams changePasswordParams, string email);
    }
}