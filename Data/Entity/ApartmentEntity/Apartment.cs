using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entity
{
    public class Apartment : BaseEntity
    {      
        [Required]
        [MaxLength(300)]
        public string HeadTitle { get; set; }
        [Required]
        [MaxLength(1000)]
        public string PropertyDescription { get; set; }
        [Required]
        public int Accommodates { get; set; }
        [Required]
        public double ApartmentSize { get; set; }
        [Required]
        public int AmountBathroom { get; set; }
        [Required]
        public int AmountBedroom { get; set; }
        [Required]
        public double ApartmentRates { get; set; }
        [Required]
        public DateTime CheckInTime { get; set; }
        [Required]
        public DateTime CheckOutTime { get; set; }

        public ICollection<Amenities> Amenities { get; set; }
        public ICollection<RulesOfResidence> RulesOfResidence { get; set; }
        public ICollection<ApartmentImage> ApartmentImages { get; set; }
        public ICollection<ApartmentReservation> ApartmentReservations { get; set; }
        public ApartmentLocation ApartmentLocation { get; set; }
        public ApartmentType ApartmentType { get; set; }
        public User Owner { get; set; }
    }
}
