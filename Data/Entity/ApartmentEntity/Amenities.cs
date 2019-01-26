using System.ComponentModel.DataAnnotations;

namespace Data.Entity
{
    public class Amenities : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name{ get; set; }

        public AmenityTypes AmenityType { get; set; }
        public Apartment Apartment { get; set; }
    }
}