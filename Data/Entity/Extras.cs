using System.ComponentModel.DataAnnotations;

namespace Data.Entity
{
    public class Extras : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Cost { get; set; }
        public Flat Flat { get; set; } = new Flat();
    }
}
