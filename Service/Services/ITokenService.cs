using System.Threading.Tasks;

namespace Service.Services
{
    public interface ITokenService
    {
        Task<string> GetTokenAsunc(string email, string password);
    }
}
