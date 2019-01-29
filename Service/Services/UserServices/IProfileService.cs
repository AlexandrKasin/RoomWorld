using System.Threading.Tasks;
using Service.DTO;

namespace Service.Services.UserServices
{
    public interface IProfileService
    {
        Task<ProfileDTO> GetProflieByEmailAsync();
        Task ChangeProfileAsync(ProfileDTO user);
        Task SendMessageResetPasswordAsync(string email);
        Task ResetPasswordByTokenAsync(ResetPasswordDTO model);
    }
}
