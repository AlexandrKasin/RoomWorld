﻿using System.ComponentModel.DataAnnotations;

namespace Service.DTO
{
    public class AuthorizeDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
