namespace SocailMediaApp.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime PublishedOn { get; set; }
        public User Author { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
