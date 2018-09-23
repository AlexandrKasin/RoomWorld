using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Service.dto
{
    public class FlatViewModel
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Accommodates { get; set; }

        [Required]
        public string SpaceOffered { get; set; }

        [Required]
        public double Size { get; set; }

        [Required]
        public DateTime CheckIn { get; set; }

        [Required]
        public DateTime CheckOut { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<AmenitiesViewModel> Amentieses { get; set; }

        public ICollection<HouseRulesViewModel> HouseRuleses { get; set; }

        public ICollection<ImageViewModel> Images { get; set; }

        public LocationViewModel Location { get; set; }
    }
}
