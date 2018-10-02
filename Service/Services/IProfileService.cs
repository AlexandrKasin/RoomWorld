using System.Threading.Tasks;
using Service.dto;

namespace Service.Services
{
    public interface IProfileService
    {
        Task<UserViewModel> GetProflieByEmailAsync(string email);
        Task ChangeProfileAsync(UserViewModel user);
    }
}
