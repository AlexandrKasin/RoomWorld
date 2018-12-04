using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entity
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public ICollection<Flat> Flats { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; }
    }
}