using System.ComponentModel.DataAnnotations;

namespace Service
{
    public class AuthorizeViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
