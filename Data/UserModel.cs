using System.ComponentModel.DataAnnotations;
using Data.Entity;

namespace Data
{
    public class UserModel : User
    {
        [Required]
        public override string Name { get; set; }

        [Required]
        public override string Surname { get; set; }

        [Required]
        public override string Role { get; set; }

        [Required]
        [EmailAddress]
        public override string Email { get; set; }

        [Required]
        public override string Password { get; set; }

        [Required]
        public override string PhoneNumber { get; set; }
    }
}
