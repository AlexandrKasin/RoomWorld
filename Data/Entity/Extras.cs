using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Data.Entity
{
    [DataContract]
    public class Extras : BaseEntity
    {
        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        [Required]
        public string Cost { get; set; }
        public Flat Flat { get; set; }
    }
}
