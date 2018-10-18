using System.Threading.Tasks;
using Service.dto;

namespace Service.Services
{
    public interface IProfileService
    {
        Task<UserViewModel> GetProflieByEmailAsync();
        Task ChangeProfileAsync(UserViewModel user);
    }
}
