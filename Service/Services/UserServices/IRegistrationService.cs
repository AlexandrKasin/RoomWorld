using System.Threading.Tasks;
using Service.DTO;
using Service.DTO.UserDTO;

namespace Service.Services.UserServices
{
    public interface IRegistrationService
    {
        Task<TokenDTO> RegisterUserAsync(UserRegistrationDTO user);
        Task ChangePasswordAsync(ChangePasswordParamsDTO changePasswordParams);
    }
}