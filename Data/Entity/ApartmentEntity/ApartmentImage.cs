using System.ComponentModel.DataAnnotations;

namespace Data.Entity.ApartmentEntity
{
    public class ApartmentImage : BaseEntity
    {
        [Required]
        public string Url { get; set; }

        public Apartment Apartment { get; set; }
    }
}
