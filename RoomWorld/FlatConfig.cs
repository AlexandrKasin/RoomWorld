using System;
using System.ComponentModel.DataAnnotations;

namespace RoomWorld
{
    public class FlatConfig
    {
        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public int Accommodates { get; set; }

       /* [Required]
        public DateTime CheckIn { get; set; }

        [Required]
        public DateTime CheckOut { get; set; }*/

    }
}
