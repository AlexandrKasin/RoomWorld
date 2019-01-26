using System.Threading.Tasks;

namespace Service.Services.UserServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
