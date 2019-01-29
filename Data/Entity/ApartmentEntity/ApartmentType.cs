using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entity.ApartmentEntity
{
    public class ApartmentType : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        public ICollection<Apartment> Apartments { get; set; }
    }
}
