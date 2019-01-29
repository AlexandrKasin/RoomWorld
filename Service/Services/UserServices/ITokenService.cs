using System.Threading.Tasks;
using Service.DTO;
using Service.DTO.UserDTO;

namespace Service.Services.UserServices
{
    public interface ITokenService
    {
        Task<TokenDTO> GetTokenAsync(AuthorizeDTO authorize);
    }
}
