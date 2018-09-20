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
        public string Type { get; set; }

        public Flat Flat { get; set; }
    }
}