namespace SocailMediaApp.ViewModels
{
    public class ReadCommentViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime PublishedOn { get; set; }
        public UserFriendViewModel Author { get; set; }
    }
}
