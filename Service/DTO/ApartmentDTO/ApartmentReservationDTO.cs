using System;

namespace Service.DTO.ApartmentDTO
{
    public class ApartmentReservationDTO
    {    
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public double SumPrice { get; set; }
        public ApartmentDTO Apartment { get; set; }
    }
}
