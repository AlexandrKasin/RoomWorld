using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entity.ChatEntity;
using Service.DTO.ChatDTO;

namespace Service.Services.ChatServices
{
    public interface IMessageService
    {
        Task AddMessageAsync(Message message);
        Task<IList<MessageDTO>> GetAllDialogsAsync();
        Task<IList<MessageDTO>> GetMessagesByEmailAsync(string email);
    }
}
