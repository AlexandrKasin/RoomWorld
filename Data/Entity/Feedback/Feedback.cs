using System.ComponentModel.DataAnnotations;
using Data.Entity.UserEntity;

namespace Data.Entity.Feedback
{
    public class Feedback : BaseEntity
    {
        [Required]
        [MaxLength(300)]
        [MinLength(20)]
        public string Text { get; set; }
        [Required]
        public int Rating { get; set; }

        public User User { get; set; }
    }
}