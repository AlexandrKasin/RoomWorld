using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entity;
using Data.Entity.ChatEntity;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Service.DTO;
using Service.DTO.ChatDTO;
using Service.Services.ChatServices;

namespace Service.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Dialog> _dialogRepository;
        private readonly IMapper _mapper;

        public MessageService(IRepository<Dialog> dialogRepository,
            IMapper mapper)
        {
            _dialogRepository = dialogRepository;
            _mapper = mapper;
        }

        public async Task AddMessageAsync(Message message)
        {
            var dialog =
                (await _dialogRepository.GetAllAsync(x => x.Client.Email == message.UserFrom.Email, x => x.Messages))
                .FirstOrDefault();
            if (dialog != null)
            {
                dialog.Messages.Add(message);
            }
            else
            {
                dialog = new Dialog {Client = message.UserFrom, Messages = new List<Message>()};
                dialog.Messages.Add(message);
            }

            await _dialogRepository.UpdateAsync(dialog);
        }

        public async Task<IList<MessageDTO>> GetAllDialogsAsync()
        {
            var dialogs = await (await _dialogRepository.GetAllAsync())
                .Include(x => x.Messages)
                .ThenInclude(x => x.UserFrom).ToListAsync();
            var listMessages = new List<Message>();
            foreach (var dialog in dialogs)
            {
                var lastMessage = dialog.Messages.LastOrDefault();
                if (lastMessage != null)
                {
                    listMessages.Add(lastMessage);
                }
            }

            return _mapper.Map<IList<MessageDTO>>(listMessages);
        }
        public async Task<IList<MessageDTO>> GetMessagesByEmailAsync(string email)
        {          
            var dialogs = (await _dialogRepository.GetAllAsync(x => x.Client.Email == email))
                .Include(x => x.Messages)
                .ThenInclude(x => x.UserFrom).FirstOrDefault();
         
            return _mapper.Map<IList<MessageDTO>>(dialogs?.Messages);
        }

        public async Task<IList<MessageDTO>> ChangeMessageAsync(string email)
        {
            var dialogs = (await _dialogRepository.GetAllAsync(x => x.Client.Email == email))
                .Include(x => x.Messages)
                .ThenInclude(x => x.UserFrom).FirstOrDefault();

            return _mapper.Map<IList<MessageDTO>>(dialogs?.Messages);
        }
    }
}