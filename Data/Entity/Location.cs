using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Data.Entity
{
    [DataContract]
    public class Location : BaseEntity
    {
        public override long Id { get; protected set; }

        [DataMember] public string Country { get; set; }

        [DataMember] public string City { get; set; }

        [DataMember] public int NumberHouse { get; set; }

        [DataMember] public int NumberHouseBlock { get; set; }

        [DataMember] public int NumberFlat { get; set; }

        [DataMember] public string GpsPoint { get; set; }

        public ICollection<Flat> Flat { get; set; }
    }
}