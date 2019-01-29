using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entity.ApartmentEntity
{
    public class AmenityTypes : BaseEntity
    {
        [Required]
        [MinLength(200)]
        public string Title { get; set; }

        public ICollection<Amenities> Amenities { get; set; }
    }
}
