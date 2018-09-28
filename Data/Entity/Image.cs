using System.ComponentModel.DataAnnotations;

namespace Data.Entity
{
    public class Image : BaseEntity
    {
        [Required]
        public string Url { get; set; }

        public Flat Flat { get; set; }
    }
}
