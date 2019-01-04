using Service.dto;

namespace Service.DTO
{
    public class MessageViewModel
    {
        public string Text { get; set; }

        public UserViewModel UserFrom { get; set; }
        public string UsernameTo { get; set; }
    }
}
