using System;
using System.Collections.Generic;

namespace Service.DTO.ApartmentDTO
{
    public class ApartmentDTO
    {
        public long Id { get; set; }
        public string HeadTitle { get; set; }
        public string PropertyDescription { get; set; }
        public int Accommodates { get; set; }
        public double ApartmentSize { get; set; }
        public int AmountBathroom { get; set; }
        public int AmountBedroom { get; set; }
        public double ApartmentRates { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public string ApartmentTypeString { get; set; }

        public ICollection<string> Images { get; set; }
        public ApartmentLocationDTO ApartmentLocation { get; set; }
        public ICollection<RulesOfResidenceDTO> RulesOfResidence { get; set; }
        public ICollection<ApartmentReservationDTO> ApartmentReservation { get; set; }
    }
}
