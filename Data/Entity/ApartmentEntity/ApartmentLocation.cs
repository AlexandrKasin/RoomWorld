using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entity.ApartmentEntity
{
    public class ApartmentLocation : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Country { get; set; }

        [Required]
        [MaxLength(200)]
        public string City { get; set; }

        [MaxLength(200)]
        public string State { get; set; }

        [Required]
        [MaxLength(400)]
        public string StreetAddress { get; set; }

        [ForeignKey("Apartment")]
        public long ApartmentId { get; set; }

        public Apartment Apartment { get; set; }
    }
}