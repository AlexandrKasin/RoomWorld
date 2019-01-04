using System;
using System.ComponentModel.DataAnnotations;

namespace Service
{
    public class OrderParamsViewModel
    {
        [Required]
        public DateTime DateFrom { get; set; }

        [Required]
        public DateTime DateTo { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public long IdFlat { get; set; }
    }
}
