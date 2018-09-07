using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Data.Entity
{
    [DataContract]
    public class Flat : BaseEntity
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public double Cost { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int Accommodates { get; set; }

        [DataMember]
        public string SpaceOffered { get; set; }

        [DataMember]
        public double Size { get; set; }


        public ICollection<Amenties> Amentieses { get; set; }
        public ICollection<Extras> Extrases { get; set; }
        public Location Location { get; set; }
        public User User { get; set; }
    }
}
