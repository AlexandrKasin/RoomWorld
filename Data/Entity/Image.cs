using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entity
{
    public class Image : BaseEntity
    {
        [Required]
        public string Url { get; set; }

        public Flat Flat { get; set; }
    }
}
