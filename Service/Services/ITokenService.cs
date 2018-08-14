using System.Threading.Tasks;

namespace Service.iServices
{
    public interface ITokenService
    {
        Task<string> GetTokenAsunc(string email, string password);
    }
}
