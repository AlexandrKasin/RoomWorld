namespace Service.DTO.ChatDTO
{
    public class MessageDTO
    {
        public string Text { get; set; }

        public UserDTO.UserDTO UserFrom { get; set; }
        public string UsernameTo { get; set; }
    }
}
