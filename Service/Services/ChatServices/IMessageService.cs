using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entity;
using Service.DTO;

namespace Service.Services
{
    public interface IMessageService
    {
        Task AddMessageAsync(Message message);
        Task<IList<MessageViewModel>> GetAllDialogsAsync();
        Task<IList<MessageViewModel>> GetMessagesByEmailAsync(string email);
    }
}
