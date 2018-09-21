using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entity
{
    public class User : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public ICollection<Flat> Flats { get; set; }
    }
}