using SocailMediaApp.Models;
using Microsoft.EntityFrameworkCore;
using SocailMediaApp.Exceptions;

namespace SocailMediaApp.Repositories
{
    public class PostRepository
    {
        private readonly SocialMediaContext _context;

        public PostRepository(SocialMediaContext context)
        {
            _context = context;
        }

        public async Task AddPost(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await _context.Posts.Include(p => p.Comments).ToListAsync();
        }

        public async Task<Post?> GetPostById(int id)
        {
            return await _context.Posts.Where(p => p.Id == id)
                                       .FirstOrDefaultAsync();
        }

        public async Task<List<Post>> GetPostsByUserId(int userId)
        {
            return await _context.Posts.Where(p => p.UserId == userId)
                                       .ToListAsync();
        }

        public async Task<Comment?> GetCommendById(int postId, int commendId)
        {
            var post = await _context.Posts.Include(p => p.Comments)
                                           .FirstOrDefaultAsync(p => p.Id == postId);
            return post?.Comments.FirstOrDefault(c => c.Id == commendId);
        }
        public async Task<List<Comment>> GetCommentsByPostId(int postId)
        {
            // Get the post with the comments
            var post = await _context.Posts.Include(p => p.Comments)
                                           .FirstOrDefaultAsync(p => p.Id == postId);
            if(post == null)
                throw new NotFoundException("Post not found");
            return post.Comments;
        }

        public async Task UpdatePost(Post post)
        {
            var existingPost = await _context.Posts.FindAsync(post.Id);
            if (existingPost != null)
            {
                existingPost.Content = post.Content;
                existingPost.PublishedOn = post.PublishedOn;
                _context.Posts.Update(existingPost);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddCommentToPost(Comment comment, Post post)
        {
            var existingPost = await _context.Posts.Include(p => p.Comments)
                                                   .FirstOrDefaultAsync(p => p.Id == post.Id);
            if (existingPost != null)
            {
                existingPost.Comments.Add(comment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCommentInPost(Comment comment, Post post)
        {
            var existingPost = await _context.Posts.Include(p => p.Comments)
                                                   .FirstOrDefaultAsync(p => p.Id == post.Id);
            var existingComment = existingPost?.Comments.FirstOrDefault(c => c.Id == comment.Id);
            if (existingComment != null)
            {
                existingComment.Content = comment.Content;
                _context.Comments.Update(existingComment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCommendtFromPost(Comment comment, Post post)
        {
            var existingPost = await _context.Posts.Include(p => p.Comments)
                                                   .FirstOrDefaultAsync(p => p.Id == post.Id);
            if (existingPost != null)
            {
                existingPost.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                var comments = await _context.Comments.Where(c => c.PostId == id)
                                                      .ToListAsync();
                _context.Comments.RemoveRange(comments);
                await _context.SaveChangesAsync();
            }
        }
    }
}
