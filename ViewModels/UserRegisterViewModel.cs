
using System.ComponentModel.DataAnnotations;

namespace SocailMediaApp.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;
        [Phone]
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}
