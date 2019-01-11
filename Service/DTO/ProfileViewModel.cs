using Microsoft.AspNetCore.Http;

namespace Service.DTO
{
    public class ProfileViewModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public IFormCollection Image { get; set; }
        public byte[] Version { get; set; }
    }
}