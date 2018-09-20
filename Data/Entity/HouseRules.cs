using System.Runtime.Serialization;

namespace Data.Entity
{
    [DataContract]
    public class HouseRules : BaseEntity
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public bool State { get; set; }

        public Flat Flat { get; set; }
    }
}
