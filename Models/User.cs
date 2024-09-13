using System.ComponentModel.DataAnnotations;

namespace SocailMediaApp.Models
{
    public class User
    {
        public int Id { get; set; }
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
        public string? ProfileImageUrl { get; set; }
        public bool EmailConfirmed { get; set; }
        [Phone]
        public string? Phone { get; set; }
        public string? Address { get; set; }

        public List<User> Following = new List<User>();
        public List<User> Followers = new List<User>();
        public List<Post> Posts = new List<Post>();
    }
}
