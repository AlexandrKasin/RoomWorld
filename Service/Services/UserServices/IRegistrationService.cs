using System.Threading.Tasks;
using Service.DTO;

namespace Service.Services.UserServices
{
    public interface IRegistrationService
    {
        Task<TokenViewModel> RegisterUserAsync(UserRegistrationParamsViewModel user);
        Task ChangePasswordAsync(ChangePasswordParamsViewModel changePasswordParams);
    }
}