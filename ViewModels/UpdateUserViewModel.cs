using System.ComponentModel.DataAnnotations;

namespace SocailMediaApp.ViewModels
{
    public class UpdateUserViewModel
    {
        [MaxLength(20)]
        [MinLength(5)]
        public string? Name { get; set; }
        [MinLength(8)]
        public string? Password { get; set; }

        [Phone]
        public string? Phone { get; set; }

        public string? Address { get; set; }
        public IFormFile? ProfileImage { get; set; }
    }
}
