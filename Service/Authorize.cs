using System.ComponentModel.DataAnnotations;

namespace Service
{
    public class Authorize
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
