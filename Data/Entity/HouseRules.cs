using System.ComponentModel.DataAnnotations;

namespace Data.Entity
{
    public class HouseRules : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public bool State { get; set; }

        public Flat Flat { get; set; }
    }
}
