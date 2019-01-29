﻿using System.ComponentModel.DataAnnotations;

namespace Service.DTO
{
    public class ResetPasswordDTO
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
