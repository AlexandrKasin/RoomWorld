using System.ComponentModel.DataAnnotations;

namespace Service.DTO.FeedbackDTO
{
    public class FeedbackDTO
    {
        [Required]
        [MaxLength(300)]
        [MinLength(20)]
        public string Text { get; set; }

        [Required]
        public int Rating { get; set; }

        public UserDTO.UserDTO User { get; set; }
    }
}