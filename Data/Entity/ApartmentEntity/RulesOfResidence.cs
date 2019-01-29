using System.ComponentModel.DataAnnotations;

namespace Data.Entity.ApartmentEntity
{
    public class RulesOfResidence : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string NameRule { get; set; }

        [Required]
        public bool IsAllowed { get; set; }

        public Apartment Apartment { get; set; }
    }
}
