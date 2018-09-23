using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Service.dto
{
    public class UserViewModel
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

        public ICollection<FlatViewModel> Flats { get; set; }
    }
}
