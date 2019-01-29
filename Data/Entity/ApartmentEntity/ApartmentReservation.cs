using System;
using System.ComponentModel.DataAnnotations;
using Data.Entity.UserEntity;

namespace Data.Entity.ApartmentEntity
{
    public class ApartmentReservation : BaseEntity
    {
        [Required]
        public DateTime DateFrom { get; set; }

        [Required]
        public DateTime DateTo { get; set; }

        [Required]
        public double SumPrice { get; set; }

        public User Client { get; set; }

        public Apartment Apartment { get; set; }
    }
}
