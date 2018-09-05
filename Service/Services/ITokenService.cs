using System.Threading.Tasks;

namespace Service.Services
{
    public interface ITokenService
    {
        Task<Token> GetTokenAsunc(Authorize authorize);
    }
}
