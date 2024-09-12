using System.ComponentModel.DataAnnotations;

namespace SocailMediaApp.ViewModels
{
    public class UserFriendViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
