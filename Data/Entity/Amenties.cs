using System.ComponentModel.DataAnnotations;

namespace Data.Entity
{
    public class Amenties : BaseEntity
    {
        [Required]
        public string Name{ get; set; }

        [Required]
        public string Type { get; set; }

        public Flat Flat { get; set; }
    }
}