using System.Threading.Tasks;
using Service.dto;
using Service.DTO;

namespace Service.Services
{
    public interface IProfileService
    {
        Task<UserViewModel> GetProflieByEmailAsync();
        Task ChangeProfileAsync(UserViewModel user);
        Task SendMessageResetPasswordAsync(string email);
        Task ResetPasswordByTokenAsync(ResetPasswordViewModel model);
    }
}
