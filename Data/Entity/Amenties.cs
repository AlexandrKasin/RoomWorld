using System.ComponentModel.DataAnnotations;

namespace Data.Entity
{
    public class Amenties : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name{ get; set; }

        [Required]
        [MaxLength(1000)]
        public string Type { get; set; }

        public Flat Flat { get; set; }
    }
}