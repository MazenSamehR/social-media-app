using SocailMediaApp.Models;

namespace SocailMediaApp.Repositories
{
    public class PostRepository
    {
        private List<Post> _posts;
        public PostRepository(List<Post> _posts) {
            this._posts = _posts;
        }
        public void AddPost(Post post)
        {
            post.Id = _posts.Count + 1;
            _posts.Add(post);
        }
        public List<Post> GetAllPosts()
        {
            return _posts;
        }
        public Post? GetPostById(int id)
        {
            return _posts.FirstOrDefault(p => p.Id == id);
        }
        public List<Post> GetPostsByUserId(int userId)
        {
            return _posts.Where(p => p.UserId == userId).ToList();
        }
        public void UpdatePost(Post post)
        {
            var existingPost = _posts.FirstOrDefault(p => p.Id == post.Id);
            if (existingPost != null)
            {
                existingPost.Content = post.Content;
                existingPost.PublishedOn = post.PublishedOn;
            }
        }
        public void DeletePost(int id)
        {
            var post = _posts.FirstOrDefault(p => p.Id == id);
            if (post != null)
            {
                _posts.Remove(post);
            }
        }
    }
}
