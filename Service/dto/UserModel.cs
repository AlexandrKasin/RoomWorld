using System.ComponentModel.DataAnnotations;

namespace Service.dto
{
    public class UserModel
    {
        [Required]
        public  string Name { get; set; }

        [Required]
        public  string Surname { get; set; }

        [Required]
        public  string Role { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public  string Password { get; set; }

        [Required]
        public  string PhoneNumber { get; set; }
    }
}
