﻿using System.ComponentModel.DataAnnotations;

namespace SocailMediaApp.ViewModels
{
    public class LoginUserViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;
    }
}
