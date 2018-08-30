using System.Collections.Generic;

namespace Data.Entity
{
    public class Location : BaseEntity
    {
        public string Country { get; set; }
        public string Sity { get; set; }
        public int NumberHouse { get; set; }
        public int NumberHouseBlock { get; set; }
        public int NumberFlat { get; set; }
        public string GpsPoint { get; set; }
        public ICollection<Flat> Flat { get; set; } = new List<Flat>();
    }
}
