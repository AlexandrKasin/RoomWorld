using System.Threading.Tasks;
using Data.Entity;
using Service.DTO;

namespace Service.Services
{
    public interface IRegistrationService
    {
        Task<Token> RegisterUserAsync(UserRegistrationParamsViewModel user);
        Task ChangePasswordAsync(ChangePasswordParams changePasswordParams);
    }
}