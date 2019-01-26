using System.ComponentModel.DataAnnotations;

namespace Data.Entity
{
    public class ApartmentImage : BaseEntity
    {
        [Required]
        public string Url { get; set; }

        public Apartment Apartment { get; set; }
    }
}
