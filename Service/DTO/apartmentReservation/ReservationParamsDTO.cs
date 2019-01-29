using System;
using System.ComponentModel.DataAnnotations;

namespace Service.DTO.apartmentReservation
{
    public class ReservationParamsDTO
    {
        [Required]
        public DateTime DateFrom { get; set; }

        [Required]
        public DateTime DateTo { get; set; }

        [Required]
        public double SumPrice { get; set; }

        [Required]
        public long IdApartment { get; set; }
    }
}
