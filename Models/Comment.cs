using System.ComponentModel.DataAnnotations.Schema;

namespace SocailMediaApp.Models
{
    [Table("comments")]
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime PublishedOn { get; set; }
        public User Author { get; set; }
        public Post Post{ get; set; }
    }
}
