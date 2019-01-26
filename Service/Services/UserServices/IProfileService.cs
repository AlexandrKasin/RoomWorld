using System.Threading.Tasks;
using Service.DTO;

namespace Service.Services.UserServices
{
    public interface IProfileService
    {
        Task<ProfileViewModel> GetProflieByEmailAsync();
        Task ChangeProfileAsync(ProfileViewModel user);
        Task SendMessageResetPasswordAsync(string email);
        Task ResetPasswordByTokenAsync(ResetPasswordViewModel model);
    }
}
