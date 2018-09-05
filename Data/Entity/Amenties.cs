using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Data.Entity
{
    [DataContract]
    public class Amenties : BaseEntity
    {
        [DataMember]
        [Required]
        public string Name{ get; set; }

        [DataMember]
        [Required]
        public bool Availability { get; set; }

        [DataMember]
        [Required]
        public int Amount { get; set; }

        [DataMember]
        [Required]
        public string Icon { get; set; }

        public Flat Flat { get; set; }
    }
}