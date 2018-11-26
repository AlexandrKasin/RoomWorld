using System.Threading.Tasks;
using Service.dto;
using Service.DTO;

namespace Service.Services
{
    public interface IProfileService
    {
        Task<ProfileViewModel> GetProflieByEmailAsync();
        Task ChangeProfileAsync(ProfileViewModel user);
        Task SendMessageResetPasswordAsync(string email);
        Task ResetPasswordByTokenAsync(ResetPasswordViewModel model);
    }
}
