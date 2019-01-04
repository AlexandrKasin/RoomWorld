using System.Threading.Tasks;
using Service.DTO;

namespace Service.Services
{
    public interface ITokenService
    {
        Task<TokenViewModel> GetTokenAsync(AuthorizeViewModel authorize);
    }
}
