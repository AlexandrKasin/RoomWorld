using System.Collections.Generic;

namespace Service.DTO.UserDTO
{
    public class TokenDTO
    {
        public string AccessToken { get; set; }
        public string Username { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}