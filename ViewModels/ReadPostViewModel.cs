namespace SocailMediaApp.ViewModels
{
    public class ReadPostViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; }
        public string? PostImageUrl { get; set; }
        public DateTime PublishedOn { get; set; }
        public List<ReadCommentViewModel> Comments { get; set; } = new List<ReadCommentViewModel>();
    }
}
