using System.Collections.Generic;

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

        public ICollection<Amenties> Amentieses { get; set; } = new List<Amenties>();
        public ICollection<Extras> Extrases { get; set; } = new List<Extras>();
        public Location Location { get; set; } = new Location();
        public User User { get; set; } = new User();
    }
}
