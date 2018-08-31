using System.ComponentModel.DataAnnotations;

namespace Data.Entity
{
    public class Amenties : BaseEntity
    {
        [Required]
        public string Name{ get; set; }

        [Required]
        public bool Availability { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public string Icon { get; set; }

        public Flat Flat { get; set; } = new Flat();
    }
}