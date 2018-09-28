using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entity
{
    
    public class Flat : BaseEntity
    {
        
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public int Accommodates { get; set; }

        [Required]
        [MaxLength(100)]
        public string SpaceOffered { get; set; }

        [Required]
        public double Size { get; set; }

        [Required]
        public int CountBathroom { get; set; }

        [Required]
        public int CountBedroom { get; set; }

        [Required]
        public DateTime CheckIn { get; set; }

        [Required]
        public DateTime CheckOut { get; set; }



        public ICollection<Amenties> Amentieses { get; set; }
        public ICollection<Extras> Extrases { get; set; }
        public ICollection<HouseRules> HouseRuleses { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Location Location { get; set; }
        public User User { get; set; }
    }
}
