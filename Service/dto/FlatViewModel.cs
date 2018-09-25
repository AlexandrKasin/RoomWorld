using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Service.dto
{
    public class FlatViewModel
    {
     
        public long Id { get; set; }

      
        public string Name { get; set; }

       
        public double Cost { get; set; }

    
        public string Description { get; set; }

       
        public int Accommodates { get; set; }

       
        public string SpaceOffered { get; set; }

      
        public double Size { get; set; }

        public int CountBathroom { get; set; }

        
        public int CountBedroom { get; set; }

        
        public DateTime CheckIn { get; set; }

       
        public DateTime CheckOut { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<AmenitiesViewModel> Amentieses { get; set; }

        public ICollection<HouseRulesViewModel> HouseRuleses { get; set; }

        public ICollection<ImageViewModel> Images { get; set; }

        public ICollection<OrderViewModel> Orders { get; set; }

        public LocationViewModel Location { get; set; }
    }
}
