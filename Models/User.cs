using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocailMediaApp.Models
{
    [Table("users")]
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

        public List<UserFollower> Following = new List<UserFollower>();
        public List<UserFollower> Followers = new List<UserFollower>();
        public List<Post> Posts = new List<Post>();
    }
}
