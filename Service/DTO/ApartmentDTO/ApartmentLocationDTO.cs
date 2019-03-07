using GeoAPI.Geometries;

namespace Service.DTO.ApartmentDTO
{
    public class ApartmentLocationDTO
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StreetAddress { get; set; }
        public ApartmentCoordinatesDTO Coordinates { get; set; }
    }
}