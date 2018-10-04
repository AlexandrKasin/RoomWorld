using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entity
{
    public class Location : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Country { get; set; }

        [Required]
        [MaxLength(200)]
        public string City { get; set; }

        [Required]
        public int NumberHouse { get; set; }

        [Required]
        public int NumberHouseBlock { get; set; }

        [Required]
        public int NumberFlat { get; set; }

        [Required]
        public string GpsPoint { get; set; }

        public ICollection<Flat> Flat { get; set; }
    }
}