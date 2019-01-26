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
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; }

        public ICollection<Apartment> Apartments { get; set; }

        public ICollection<ApartmentReservation> ApartmentReservations { get; set; }

        /*public ICollection<Message> Messages { get; set; }*/

        /*public ICollection<Dialog> Dialogs { get; set; }*/
    }
}