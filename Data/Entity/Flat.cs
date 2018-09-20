using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Data.Entity
{
    
    public class Flat : BaseEntity
    {
        
        public string Name { get; set; }

        
        public double Cost { get; set; }

        
        public string Description { get; set; }

       
        public int Accommodates { get; set; }

        
        public string SpaceOffered { get; set; }

     
        public double Size { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }



        public ICollection<Amenties> Amentieses { get; set; }
        public ICollection<Extras> Extrases { get; set; }
        public ICollection<HouseRules> HouseRuleses { get; set; }
        public Location Location { get; set; }
        public User User { get; set; }
    }
}
