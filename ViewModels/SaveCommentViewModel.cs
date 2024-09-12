using System.ComponentModel.DataAnnotations;

namespace SocailMediaApp.ViewModels
{
    public class SaveCommentViewModel
    {
        [Required]
        public string Content { get; set; } = string.Empty;
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}
