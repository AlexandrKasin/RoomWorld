using System.ComponentModel.DataAnnotations;

namespace Service.dto
{
    public class LocationViewModel
    {
        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public int NumberHouse { get; set; }

        [Required]
        public int NumberHouseBlock { get; set; }

        [Required]
        public int NumberFlat { get; set; }

        [Required]
        public string GpsPoint { get; set; }
    }
}
