using System.Threading.Tasks;
using Service.DTO;

namespace Service.Services
{
    public interface ITokenService
    {
        Task<Token> GetTokenAsync(AuthorizeViewModel authorize);
    }
}
